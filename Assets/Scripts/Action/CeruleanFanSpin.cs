using System;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using Random = System.Random;

namespace Action
{
    public class CeruleanFanSpin : CeruleanActionBase
    {

        public CeruleanFanSpin(CeruleanController contr) : base(contr)
        {
        }

        public override void Init()
        {
            phase = 0;
            targetSelected = false;
            PointsAlongWay.Clear();
            ArrivedAt.Clear();
            phaseTwoSelected = false;
            TimeInPhaseTwo = 0f;
        }
        
        private int phase = 0;
        
        // Phase 0 - Movement to area above Pastel
        private Boolean targetSelected = false;
        public float Phase0MoveSpeed = 30f;
        public float MidpointVariance = 6.75f;
        private List<Vector2> PointsAlongWay = new List<Vector2>();
        private List<Boolean> ArrivedAt = new List<Boolean>();
        private int NumberPointsToHave = 6;

        public float Angle = 30.0f;
        
        // Phase 2 - First two rays of rocky death
        public Vector2 targetOne, targetTwo;
        public Vector2 targetThree, targetFour;
        public Boolean phaseTwoSelected = false;
        public float Spread = 2f;
        private float TimeInPhaseTwo = 0f;
        private float TimeToSpendInPhaseTwo = 4.75f;

        public override bool IsFinished()
        {
            return phase == 3;
        }

        public override bool CanRun()
        {
            return controller.lastRunCode != getCode();
        }

        public override String getCode()
        {
            return "fanspin";
        }

        public override int GetPriority()
        {
            return 3;
        }

        public override void Update()
        {
            if (phase == 0)
            {
                // Fly towards the target point. AKA, above Pastel when this method FIRST runs. So she can move.
                if (!targetSelected)
                {
                    // Select the target
                    Vector2 final = new Vector2(controller.Pastel.transform.position.x, 15.75f);
                    for (int i = 0; i < NumberPointsToHave - 1; i++)
                    {
                        float cumFraction = i / (float)NumberPointsToHave;
                        Vector2 LerpRes = Vector2.Lerp(MyPos(), final, cumFraction);
                        LerpRes.y += 6 / Mathf.Sqrt(i + 1);
                        PointsAlongWay.Add(LerpRes);
                        
                        ArrivedAt.Add(false);
                    }
                    PointsAlongWay.Add(final);
                    ArrivedAt.Add(false);
                    
                    targetSelected = true;
                    return;
                }

                int currentOne = 0;
                for (; currentOne < NumberPointsToHave; currentOne++)
                {
                    if (!ArrivedAt[currentOne])
                    {
                        break;
                    }
                }

                if (currentOne == NumberPointsToHave)
                {
                    phase = 1;
                    return;
                }
                
                Vector2 changeVector = Phase0MoveSpeed * (PointsAlongWay[currentOne] - new Vector2(controller.gameObject.transform.position.x,
                                                              controller.gameObject.transform.position.y)).normalized;

                // Move us towards that point
                controller.gameObject.GetComponent<Rigidbody2D>().velocity = changeVector;

                if (Vector2.Distance(
                        new Vector2(controller.gameObject.transform.position.x,
                            controller.gameObject.transform.position.y), PointsAlongWay[currentOne]) <= 0.5f)
                {
                    ArrivedAt[currentOne] = true;
                    return;
                }
            }

            if (phase == 1)
            {
                controller.gameObject.GetComponent<Rigidbody2D>().velocity = 
                    Vector2.Lerp(controller.gameObject.GetComponent<Rigidbody2D>().velocity, 
                        new Vector2(0f, 0f), 0.4f);

                if (RB().velocity.magnitude <= 0.1)
                {
                    phase = 2;
                    RB().velocity = new Vector2();
                    return;
                }
            }

            if (phase == 2)
            {
                if (!phaseTwoSelected)
                {
                    // We want to begin shooting the fan.
                    // We need to tabulate two points at a thirty degree angle
                    // adjacent * tan(angle) = length of opposite
                    float opp = MyPos().y * Mathf.Tan(Angle * Mathf.Deg2Rad);
                    targetOne = new Vector2(PastelPos().x + opp, 0f);
                    targetTwo = new Vector2(PastelPos().x - opp, 0f);
                    float opp2 = MyPos().y * Mathf.Tan(Angle * 2 * Mathf.Deg2Rad);
                    targetThree = new Vector2(PastelPos().x + opp2, 0f);
                    targetFour = new Vector2(PastelPos().x - opp2, 0f);
                    phaseTwoSelected = true;
                    return;
                }

                TimeInPhaseTwo += Time.deltaTime;
                if (TimeInPhaseTwo > TimeToSpendInPhaseTwo)
                {
                    phase = 3;
                    return;
                }
                SpawnRock(controller.transform.position + Vector3.right, targetOne);
                SpawnRock(controller.transform.position + Vector3.left, targetTwo);
                SpawnRock(controller.transform.position + Vector3.right + Vector3.up, targetThree);
                SpawnRock(controller.transform.position + Vector3.left + Vector3.up, targetFour);
            }
        }
    }
}
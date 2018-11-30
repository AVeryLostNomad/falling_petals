using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;

namespace Action
{
    public class CeruleanLeftCorner : CeruleanActionBase
    {
        private int phase = 0;
        private bool targetSelected = false;
        public float Phase0MoveSpeed = 30f;
        private List<Vector2> PointsAlongWay = new List<Vector2>();
        private List<Boolean> ArrivedAt = new List<Boolean>();
        private int NumberPointsToHave = 6;

        public Boolean phaseTwoSelected = false;
        public Vector2 targetOne, targetTwo, targetThree;
        private float TimeInPhaseTwo = 0f;
        private float TimeToSpendInPhaseTwo = 4.75f;
        
        public CeruleanLeftCorner(CeruleanController contr) : base(contr)
        {
        }

        public override void Init()
        {
            phase = 0;
            targetSelected = false;
            PointsAlongWay.Clear();
            ArrivedAt.Clear();
            TimeInPhaseTwo = 0f;
        }

        public override string getCode()
        {
            return "leftcorner";
        }

        public override bool IsFinished()
        {
            return phase == 3;
        }

        public override bool CanRun()
        {
            return controller.lastRunCode != getCode();
        }

        public override int GetPriority()
        {
            return 3;
        }

        public override void Update()
        {
            if (phase == 0)
            {
                if (!targetSelected)
                {
                    Vector2 final = new Vector2(-32.5f, 16.1f);
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
                // Throw some rocks
                if (!phaseTwoSelected)
                {
                    float opp = MyPos().y * Mathf.Tan(30 * Mathf.Deg2Rad);
                    targetOne = new Vector2(MyPos().x + opp, 0f);
                    float opp2 = MyPos().y * Mathf.Tan(45 * Mathf.Deg2Rad);
                    targetTwo = new Vector2(MyPos().x + opp2, 0f);
                    float opp3 = MyPos().y * Mathf.Tan(75 * Mathf.Deg2Rad);
                    targetThree = new Vector2(MyPos().x + opp3, 0f);
                    phaseTwoSelected = true;
                    return;
                }

                TimeInPhaseTwo += Time.deltaTime;
                if (TimeInPhaseTwo > TimeToSpendInPhaseTwo)
                {
                    phase = 3;
                    return;
                }

                SpawnRock(controller.transform.position + Vector3.right, targetThree);
                SpawnRock(controller.transform.position + Vector3.right + Vector3.down, targetTwo);
                SpawnRock(controller.transform.position + Vector3.right + Vector3.down + Vector3.down, targetOne);
            }
        }
    }
}
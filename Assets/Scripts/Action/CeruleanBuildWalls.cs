using System;
using System.Collections.Generic;
using UnityEngine;

namespace Action
{
    public class CeruleanBuildWalls : CeruleanActionBase
    {
        public CeruleanBuildWalls(CeruleanController contr) : base(contr)
        {
        }

        public override string getCode()
        {
            return "walls";
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
            return 2;
        }

        public override void Update()
        {
            if (phase == 0)
            {
                if (!targetSelected)
                {
                    // Select the target
                    Vector2 final = new Vector2(Math.Min(controller.Pastel.transform.position.x + 5f, 29.24f), 15.75f);
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

                TimeInPhaseTwo += Time.deltaTime;
                if (TimeInPhaseTwo > TimeToSpendInPhaseTwo)
                {
                    phase = 3;
                    return;
                }
                
                // Start dumping rocks downwards.
                SpawnRock(controller.transform.position + Vector3.down, 
                    new Vector3(MyPos().x, 0f, 0f));
            }
        }

        private int phase = 0;
        
        // Phase 0 - Movement to area to the right of pastel
        private Boolean targetSelected = false;
        public float Phase0MoveSpeed = 30f;
        private List<Vector2> PointsAlongWay = new List<Vector2>();
        private List<Boolean> ArrivedAt = new List<Boolean>();
        private int NumberPointsToHave = 6;
        private float TimeInPhaseTwo = 0f;
        private float TimeToSpendInPhaseTwo = 4.75f;

        public override void Init()
        {
            phase = 0;
            PointsAlongWay.Clear();
            ArrivedAt.Clear();
            TimeInPhaseTwo = 0f;
        }
    }
}
using System;
using UnityEngine;

namespace Action
{
    public abstract class ActionBase
    {
        protected CeruleanController controller;

        protected ActionBase(CeruleanController contr)
        {
            controller = contr;
        }

        public abstract String getCode();

        public abstract bool IsFinished();

        public abstract bool CanRun();

        public abstract int GetPriority();
        
        public abstract void Update();

        public abstract void Init();

        public Vector2 PastelPos()
        {
            return new Vector2(controller.Pastel.transform.position.x, controller.Pastel.transform.position.y);
        }

        public Rigidbody2D RB()
        {
            return controller.GetComponent<Rigidbody2D>();
        }

        public Vector2 MyPos()
        {
            return new Vector2(controller.transform.position.x, controller.transform.position.y);
        }
        
    }
}
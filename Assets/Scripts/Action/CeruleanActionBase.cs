using UnityEngine;

namespace Action
{
    public abstract class CeruleanActionBase : ActionBase
    {
        protected CeruleanActionBase(CeruleanController contr) : base(contr)
        {
        }

        public void SpawnRock(Vector3 startPos, Vector2 targetSpot)
        {
            GameObject pebble = controller.MakePebble();
            Physics2D.IgnoreCollision(pebble.GetComponent<BoxCollider2D>(),
                controller.GetComponent<PolygonCollider2D>());
            pebble.transform.SetPositionAndRotation(startPos, new Quaternion());
            Vector2 target = targetSpot + UnityEngine.Random.insideUnitCircle * 2f;
            pebble.GetComponent<Rigidbody2D>().velocity += target - MyPos();
        }
        
    }
}
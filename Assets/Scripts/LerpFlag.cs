using UnityEngine;

namespace DefaultNamespace
{
    
    public class LerpFlag : MonoBehaviour
    {
        public GradualLerper Lerper;
        public float value;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Lerper.TargetCameraOrtho = value;
        }
    }
}
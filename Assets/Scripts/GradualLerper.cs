using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class GradualLerper : MonoBehaviour
    {
        public float LerpRate;
        public float TargetCameraOrtho;

        private void Start()
        {
            TargetCameraOrtho = GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
        }

        private void Update()
        {
            CinemachineVirtualCamera cvc = GetComponent<CinemachineVirtualCamera>();
            cvc.m_Lens.OrthographicSize = Mathf.Lerp(cvc.m_Lens.OrthographicSize, TargetCameraOrtho, LerpRate);
        }
    }
}
using UnityEngine;

public class TeeterTotter : MonoBehaviour
{
    public float VigorousLerping = 0.2f;
    public float AngleSafety = 23f;
    
    private void Update()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.z) < AngleSafety)
        {
            // Lerp us towards standing up straight
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z,
                Mathf.Lerp(transform.rotation.eulerAngles.z, 0f, VigorousLerping));
        }
    }
}
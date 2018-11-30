using UnityEngine;

public class DestroyLater : MonoBehaviour
{

    private float _timeExisted;
    public float ExistFor;
    
    private void Update()
    {
        _timeExisted += Time.deltaTime;
        if (_timeExisted > ExistFor)
        {
            Destroy(gameObject);
        }
    }
}
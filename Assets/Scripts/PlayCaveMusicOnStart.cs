using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayCaveMusicOnStart : MonoBehaviour
{

    public Move movecontroller;
    public CinemachineVirtualCamera Camera;
    public float StopAt = 800.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        movecontroller.Disabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movecontroller.gameObject.transform.position.x >= StopAt)
        {
            movecontroller.StopRun();
            Camera.Follow = null;
            return;
        }
        movecontroller.RunRight();
    }
}

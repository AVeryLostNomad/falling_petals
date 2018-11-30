using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float MoveSpeed = 1.5f;
    public Animator Anim;
    public Transform trans;

    public bool Disabled = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    public void StopRun()
    {
        Anim.SetBool("RunRight0", false);
    }

    public void RunLeft()
    {
        trans.SetPositionAndRotation(trans.position, Quaternion.Euler(0f, 180f, 0f));
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
        Anim.SetBool("RunRight0", true);
    }

    public void RunRight()
    {
        trans.SetPositionAndRotation(trans.position, Quaternion.Euler(0f, 0f, 0f));
        transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
        Anim.SetBool("RunRight0", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Disabled) return;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RunLeft();
        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            RunRight();
        }
        else
        {
            Anim.SetBool("RunRight0", false);
        }
    }
}

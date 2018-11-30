using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class BetterJump : MonoBehaviour
{

    public float FallMultiplier = 2.5f; // Multiply gravity when falling
    public float LowJumpMultiplier = 2f;
    public float UpMultiplier = 1.2f;

    public float Jumps = 2;
    private int CurrentJumps = 0;

    public float jumpVelocity;

    private Rigidbody2D rb;
    public Animator anim;

    public float lastVelocityY = 0f;

    public bool CanFall = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(transform.position, transform.position + Vector3.down * 2.5f);
        foreach (Collider2D c2d in colliders)
        {
            if (c2d.gameObject.GetComponent<Floor>() != null) return true;
        }

        return false;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            CurrentJumps = 0;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (CurrentJumps >= Jumps) return;
            //if (CurrentJumps == 0 && !IsGrounded()) return; // TODO No midair jumps for you, unless maybe we have an upgrade? TODO
            CurrentJumps += 1;
            rb.velocity = Vector2.up * jumpVelocity;
            anim.SetBool("Jumping", true);
            CanFall = true;
        }
        
        if (lastVelocityY <= 0 && Mathf.Abs(rb.velocity.y) <= 0.15)
        {
            // Was falling
            anim.SetBool("Falling", false);
            CanFall = false;
        }else if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1f) * Time.deltaTime;
            anim.SetBool("Jumping", false); // We're not jumping anymore
            if(CanFall) anim.SetBool("Falling", true);
        }else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Z))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1f) * Time.deltaTime;
        }
        else
        {
            rb.velocity += Vector2.up * UpMultiplier * Time.deltaTime;
        }

        lastVelocityY = rb.velocity.y;
    }
}

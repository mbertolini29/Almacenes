using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerAnimController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speed = rb.velocity.normalized;
        bool isWalking = speed.sqrMagnitude > 0;
        if (isWalking)
        {
            animator.SetFloat("speedX", speed.x);
            animator.SetFloat("speedY", speed.y);
        }
        animator.SetBool("isWalking", isWalking);
    }
}

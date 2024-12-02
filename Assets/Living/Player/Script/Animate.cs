using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    void FixedUpdate()
    {        
        float moveInput = Input.GetAxis("Vertical");
        float moveInput2 = Input.GetAxis("Horizontal");
        
        if (moveInput != 0f || moveInput2 != 0f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
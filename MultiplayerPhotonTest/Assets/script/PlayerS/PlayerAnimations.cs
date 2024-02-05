using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
  Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Run()
    {
        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);
    }   
    public void Idle()
    {
        animator.SetBool("Move", false);
    }

    public void Attack()
    {
        animator.SetBool("Move", false);
        animator.SetTrigger("Attack");
        
    }

}

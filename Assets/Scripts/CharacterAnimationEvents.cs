using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    public Animator animator;
    
    private Rigidbody2D _parentrb;
    
    // private static readonly int VelocityX = animator.StringToHash("velocityX");
    // private static readonly int VelocityY = Animator.StringToHash("VelocityY");

    public void Start()
    {
        _parentrb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        setVelocity(_parentrb.velocity);
    }

    public void idle()
    {
        ;
    }
    
    public void walk(Vector2 inputVec)
    {
        ;
    }

    public void hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void attack()
    {
        animator.SetTrigger("Attack");
    }

    public void die()
    {
        animator.SetTrigger("Dead");
    }

    public void revive()
    {
        animator.SetTrigger("Revive");
    }

    public void setVelocity(Vector2 inputVec)
    {
        float x = inputVec.x;
        float y = inputVec.y;
        // if(inputVec.x != 0 || inputVec.y != 0)
        //     Debug.Log("velocityX:" + x + " velocityY:" + y);
        
        animator.SetFloat("velocityX", x);
        animator.SetFloat("velocityY", y);
    }
}

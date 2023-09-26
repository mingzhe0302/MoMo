using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Animator)))]
public class WeaponAnimationEvents : MonoBehaviour
{
    public Animator animator;

    public void fire()
    {
        animator.SetTrigger("isFiring");
    }
}

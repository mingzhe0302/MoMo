using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseCharacterBehaviour
{
    private Transform targetTransform;
    private Transform _aimmedTransform;
    private bool _flipX; 
    
    
    public GameObject aimmedGameObject;
    public bool flipWhenRotate;
    public bool hasAttackAnimation;
    public EnemyAI enemyAI;

    public float argoRange;
    public float keptDistance;
    
    
    private new void Start()
    {
        base.Start();
        
        targetTransform = GetComponentInChildren<WeaponTarget>().transform;
        _aimmedTransform = aimmedGameObject.transform;

        if (hasAttackAnimation)
            targetTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    new void Update()
    {
        if (health.isDead() && !deathStatus)
        {
            deathStatus = true;
            kill();
        }
    }

    private void FixedUpdate()
    {
        move();

        if (!deathStatus)
        {
            moveTarget(_aimmedTransform.position);
            
            // Debug.Log(enemyAI.attackBool(targetTransform.position, 
            //         _aimmedTransform.position, 
            //         currentWeapon.getRange(), 
            //         currentWeapon.getWeaponType().Equals("Hitbox")? 0: keptDistance));
            
            if (enemyAI.attackBool(targetTransform.position, 
                    _aimmedTransform.position, 
                    currentWeapon.getWeaponType().Equals("Hitbox")? 2: argoRange,
                    currentWeapon.getWeaponType().Equals("Hitbox")? 0: keptDistance-0.5f))
                attack();
            
            
            if (flipWhenRotate)
            {
//                switch ((- transform.position + _aimmedTransform.position).x)
                switch (characterMovement.rb.velocity.x)
                {
                    case < 0 when !_flipX:
                    case > 0 when _flipX:
                        flip();
                        break;
                }
            }
            
        }
    }

    public override void move()
    {
        // characterMovement.move((- transform.position + _aimmedTransform.position).normalized);
        if(!deathStatus)
            characterMovement.move(enemyAI.movementVector(transform.position, _aimmedTransform.position, argoRange, keptDistance));
        else
            characterMovement.move(Vector2.zero);
    }

    public override void attack()
    {
        if(hasAttackAnimation && weaponAttack.attack(currentWeapon ,targetTransform.position, !hasAttackAnimation))
            _characterAnimationEvents.attack();
    }

    public override void rotate()
    {
        ;
    }
    
    public void flip()
    {
        _flipX = !_flipX;
        _spriteRenderer.flipX = _flipX;

        // transform.localScale = Vector3.Scale(transform.localScale ,new Vector3(-1f, 1f, 1f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, argoRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, keptDistance);
    }
}

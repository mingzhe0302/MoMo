using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class Player : BaseCharacterBehaviour
{
    private bool _flipX;
    private Transform targetTransform;

    private new void Start()
    {
        base.Start();
        targetTransform = GetComponentInChildren<WeaponTarget>().transform;
    }

    private new void Update()
    {
        if(_firstRun)
            base.Update();
        
        if (health.isDead() && !deathStatus)
        {
            deathStatus = true;
            kill();
            
            revive();
        }

        else if (!deathStatus)
        {
            if (PlayerInput.isFiring())
            {
                attack();
            }

            if (PlayerInput.weaponSwitch())
            {
                cycleWeapon();
            }

            if (PlayerInput.getPlayerInputVector().x < 0 && !_flipX)
                flip();
            else if (PlayerInput.getPlayerInputVector().x > 0 && _flipX)
                flip();
        }
    }
    
    private void FixedUpdate()
    {
        if (!deathStatus)
        {
            move();
            moveTarget(PlayerInput.getCursorPosToMouse());
        }
    }

    public override void move()
    {
        characterMovement.move(PlayerInput.getPlayerInputVector());
    }

    public override void rotate()
    {
        characterMovement.rotate_to_point(PlayerInput.getCursorPosToMouse());
    }

    // public override void cycleWeapon()
    // {
    //     currentWeapon = equipmentList.cycleWeapon();
    //     displayedWeapon.UpdateWeapon(currentWeapon);
    // }

    public override void attack()
    {
        weaponAttack.attack(currentWeapon, targetTransform, true);
    }

    public void flip()
    {
        _flipX = !_flipX;
        _spriteRenderer.flipX = _flipX;

        // transform.localScale = Vector3.Scale(transform.localScale ,new Vector3(-1f, 1f, 1f));
    }
    
    public override void kill()
    {
        base.kill();
        Debug.Log("Player Dead");
    }



    // private void OnDrawGizmos()
    // {
    //     if (currentWeapon.getWeaponType() == "Hitbox")
    //     {
    //         Hitbox hBox = (Hitbox)currentWeapon;
    //         Gizmos.DrawWireSphere(transform.position + new Vector3(hBox.offsetFromCenter.x, hBox.offsetFromCenter.y, 0), hBox.circleScale);
    //     }
    // }
}

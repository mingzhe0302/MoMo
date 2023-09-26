using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapon;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseCharacterBehaviour : MonoBehaviour, IDamageable, IBaseCharacterBehaviour
{
    public Health health;
    public CharacterMovement characterMovement;

    public EquipmentList equipmentList;
    public WeaponBaseClass currentWeapon;

    public bool destroyAfterDeath;
    public float deathDestoryDelay = 2;

    [NonSerialized]
    public WeaponAttack weaponAttack;
    [NonSerialized]
    public WeaponTarget weaponTarget;
    [NonSerialized]
    public DisplayWeapon displayedWeapon;

    protected SpriteRenderer _spriteRenderer;
    protected CharacterAnimationEvents _characterAnimationEvents;
    protected bool deathStatus = false;


    protected bool _firstRun = true;
    protected virtual void Start()
    {
        characterMovement.rb = this.GetComponent<Rigidbody2D>();

        // equipmentList = GetComponent<EquipmentList>();
        weaponAttack = GetComponent<WeaponAttack>();

        weaponTarget = GetComponentInChildren<WeaponTarget>();
        
        currentWeapon = equipmentList.getFirstWeapon();
        displayedWeapon = GetComponentInChildren<DisplayWeapon>();
        displayedWeapon.UpdateWeapon(currentWeapon);

        _characterAnimationEvents = GetComponent<CharacterAnimationEvents>();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        if (_firstRun)
        {
            cycleWeapon();
            _firstRun = false;
        }
    }

    public abstract void attack();
    public abstract void move();
    public abstract void rotate();
    
    public virtual void cycleWeapon()
    {
        currentWeapon = equipmentList.cycleWeapon();
        displayedWeapon.UpdateWeapon(currentWeapon);
    }

    public void damage(float amount)
    {
        _characterAnimationEvents.hurt();
        health.modifyHealth(-amount);
    }

    public virtual void kill()
    {
        _characterAnimationEvents.die();

        if(destroyAfterDeath)
            StartCoroutine(deathDestory(deathDestoryDelay));
    }

    public virtual void revive()
    {
        StartCoroutine(reviveWait());
    }

    public Health getHealth()
    {
        return health;
    }

    public IEnumerator reviveWait()
    {
        yield return new WaitForSeconds(2);
        health.fullHealth();
        deathStatus = false;
        
        _characterAnimationEvents.revive();
    }

    IEnumerator deathDestory(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
    
    //move target (used for weapon targeting) to point
    public void moveTarget(Vector2 targetPosition)
    {
        Transform t = transform;
        if (currentWeapon != null)
        {
            if (currentWeapon.getWeaponType().Equals("Hitbox"))
            {
                float range = currentWeapon.getRange();
                float size = currentWeapon.getSize();
                weaponTarget.moveTargetToPos(t, targetPosition, range, size); //move target within range
            }
            else if (currentWeapon.getWeaponType().Equals("Hitscan")) // || currentWeapon.getWeaponType().Equals("Projectile"))
            {
                float range = 0.5f;
                float size = 1;
                weaponTarget.moveTargetToPos(t, targetPosition, range, size);
            }
            else
            {
                float range = 0.5f;
                float size = 1;
                weaponTarget.moveTargetToPos(t, targetPosition, range, size);
            }
        }
    }
}

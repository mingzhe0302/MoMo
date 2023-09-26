using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using UnityEngine;
using Weapon;
using Vector2 = System.Numerics.Vector2;


[Serializable]
//implement attack of weapons
public class WeaponAttack : MonoBehaviour
{
    private float timePassed = 0f;
    private WeaponAnimationEvents _weaponAnimationEvents;

    public GameObject firedProjectile;

    private void Start()
    {
        _weaponAnimationEvents = GetComponentInChildren<WeaponAnimationEvents>();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
    }

    public bool attack(WeaponBaseClass weapon, UnityEngine.Vector2 targetPos)
    {
        if (timePassed > weapon.attackrate)
        {
            timePassed = 0;
            playFiringAni();
            useWeapon(weapon, targetPos);
            return true;
        }
        
        return false;
    }
    
    //return true when successfully attacked
    public bool attack(WeaponBaseClass weapon, Transform targetTransform)
    {
        if (timePassed > weapon.attackrate)
        {
            timePassed = 0;
            playFiringAni();
            useWeapon(weapon, targetTransform);
            return true;
        }

        return false;
    }
    
    public void attack(WeaponBaseClass weapon)
    {
        if (timePassed > weapon.attackrate)
        {
            timePassed = 0;
            useWeapon(weapon);
        }
    }
    
    private void useWeapon(WeaponBaseClass weapon, UnityEngine.Vector3 targetPos)
    {
        string parentTag = GetComponentInParent<Transform>().tag;
        
        if(weapon.getWeaponType() == null)
            return;
        
        else if (weapon.getWeaponType().Equals("Hitbox"))
        {
            float targetSize = weapon.getSize();
            Collider2D[] cols = Physics2D.OverlapCircleAll(targetPos, targetSize);

            foreach (var c in cols)
            {
                string colTag = c.gameObject.tag;

                //only attacks 
                if(!colTag.Equals(parentTag) && c.gameObject.layer == LayerMask.NameToLayer("Damageable"))
                    if (c.TryGetComponent(out IDamageable hit))
                        hit.damage(weapon.damage);
                    else
                        Debug.Log("Not Damageable!");
            }
        }
        
        else if (weapon.getWeaponType().Equals("Hitscan"))
        {
            var currentposition = transform.position;
            Vector3 direction = -currentposition + targetPos;
            
            Debug.DrawRay(currentposition, (direction)*100, Color.red, 5f);

            RaycastHit2D[] cols = Physics2D.RaycastAll(currentposition, (direction));

            if (cols != null)
            {
                foreach (var rc2D in cols)
                {
                    GameObject g = rc2D.transform.gameObject;
                    if (!g.tag.Equals(parentTag) && 
                        rc2D.transform.TryGetComponent(out IDamageable hit) &&
                        g.layer == LayerMask.NameToLayer("Damageable"))
                    {
                        Debug.Log(rc2D.transform.gameObject);
                        hit.damage(weapon.damage);
                    }
                }
            }
        }
        else if (weapon.getWeaponType().Equals("Projectile"))   //throw out projectile
        {
            var currentposition = transform.position;
            Vector3 direction = -currentposition + targetPos;

            GameObject proj = firedProjectile;

            if (proj != null)
            {
                GameObject firedProj = GameObject.Instantiate(proj);
                float angle = Vector3.SignedAngle(Vector3.right, direction, new Vector3(0,0,1));
                firedProj.transform.eulerAngles = new Vector3(0, 0, angle);
                firedProj.transform.position = targetPos + direction *weapon.getProjectileSpeed();
                firedProj.GetComponent<Rigidbody2D>().AddForce(direction*weapon.getProjectileSpeed(),ForceMode2D.Impulse);
            }
        }

        else
        {
            throw new ApplicationException("Weapon type does not exist!");
        }
    }
    private void useWeapon(WeaponBaseClass weapon, Transform targetTransform)
    {
        //attacks on target position
        Vector3 targetPos = targetTransform.position;
        
        string parentTag = GetComponentInParent<Transform>().tag;
        
        if(weapon.getWeaponType() == null)
            return;
        
        else if (weapon.getWeaponType().Equals("Hitbox"))
        {
            float targetSize = weapon.getSize();
            Collider2D[] cols = Physics2D.OverlapCircleAll(targetPos, targetSize);

            foreach (var c in cols)
            {
                string colTag = c.gameObject.tag;

                //only attacks 
                if(!colTag.Equals(parentTag) && c.gameObject.layer == LayerMask.NameToLayer("Damageable"))
                    if (c.TryGetComponent(out IDamageable hit))
                        hit.damage(weapon.damage);
                    else
                        Debug.Log("Not Damageable!");
            }
        }
        
        else if (weapon.getWeaponType().Equals("Hitscan"))
        {
            var currentposition = transform.position;
            Vector3 direction = -currentposition + targetPos;
            
            Debug.DrawRay(currentposition, (direction)*100, Color.red, 5f);

            RaycastHit2D[] cols = Physics2D.RaycastAll(currentposition, (direction));

            if (cols != null)
            {
                foreach (var rc2D in cols)
                {
                    GameObject g = rc2D.transform.gameObject;
                    if (!g.tag.Equals(parentTag) && 
                        rc2D.transform.TryGetComponent(out IDamageable hit) &&
                        g.layer == LayerMask.NameToLayer("Damageable"))
                    {
                        //Debug.Log(rc2D.transform.gameObject);
                        hit.damage(weapon.damage);
                    }
                }
            }
        }
        else if (weapon.getWeaponType().Equals("Projectile"))   //throw out projectile
        {
            var currentposition = transform.position;
            Vector3 direction = -currentposition + targetPos;

            GameObject proj = firedProjectile;

            if (proj != null)
            {
                GameObject firedProj = GameObject.Instantiate(proj);
                float angle = Vector3.SignedAngle(Vector3.right, direction, new Vector3(0,0,1));
                firedProj.transform.eulerAngles = new Vector3(0, 0, angle);
                firedProj.transform.position = targetPos + direction *weapon.getProjectileSpeed();
                firedProj.GetComponent<Rigidbody2D>().AddForce(direction*weapon.getProjectileSpeed(),ForceMode2D.Impulse);
            }
        }

        else
        {
            throw new ApplicationException("Weapon type does not exist!");
        }
    }

    private void useWeapon(WeaponBaseClass weapon)
    {
        if(weapon.getWeaponType() == null)
            return;
        
        else if (weapon.getWeaponType().Equals("Hitbox"))
        {
            Hitbox hBox = (Hitbox)weapon;
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position + new Vector3(hBox.offsetFromCenter.x, hBox.offsetFromCenter.y, 0), hBox.circleScale);
        }
    }

    public void playFiringAni()
    {
        _weaponAnimationEvents.fire();
    }

}

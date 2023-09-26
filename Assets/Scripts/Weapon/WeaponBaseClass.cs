using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class WeaponBaseClass : ScriptableObject
    {
        public string weaponName;
        public string weaponDesc;
        public float attackrate;
        public float damage;
        
        public RuntimeAnimatorController weaponAnimationController;
        public Sprite sprite;

        public virtual string getWeaponType()
        {
            return null;
        }

        public virtual float getRange()
        {
            return 0;
        }

        public virtual float getSize()
        {
            return 0;
        }

        public virtual float getProjectileSpeed()
        {
            return 0;
        }
    }
}
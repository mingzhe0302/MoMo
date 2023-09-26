using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu]
    public class Projectile : WeaponBaseClass
    {
        public string weaponType = "Projectile";
        public float projectileSpeed;

        public override string getWeaponType()
        {
            return weaponType;
        }

        public override float getProjectileSpeed()
        {
            return projectileSpeed;
        }
    }
}
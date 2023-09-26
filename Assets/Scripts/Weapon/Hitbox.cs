using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu]
    public class Hitbox : WeaponBaseClass
    {
        [SerializeField]
        public string weaponType = "Hitbox";
        public float range;
        public float size;

        public override string getWeaponType()
        {
            return weaponType;
        }
        
        public override float getRange()
        {
            return range;
        }

        public override float getSize()
        {
            return size;
        }
    }
}
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu]
    public class Hitscan : WeaponBaseClass
    {
        public string weaponType = "Hitscan";
        
        public override string getWeaponType()
        {
            return weaponType;
        }
    }
}
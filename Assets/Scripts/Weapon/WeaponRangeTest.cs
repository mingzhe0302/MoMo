using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class WeaponRangeTest : MonoBehaviour
{

    public List<Hitbox> weaponList;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, weaponList[0].getRange());    
    }
}

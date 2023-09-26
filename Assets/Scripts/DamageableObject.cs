using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    public Health health;


    public void damage(float amount)
    {
        health.modifyHealth(-amount);
        
        if(health.health <= 0)
            kill();
        
        Debug.Log(health.health);
    }

    public void kill()
    {
        Debug.Log("Object Destroyed");
        StartCoroutine(ObjDes());
    }

    IEnumerator ObjDes()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    public Health getHealth()
    {
        return health;
    }
}

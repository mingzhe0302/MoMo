using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Health
{
    public float maxHealth;
    public float health;

    public void fullHealth()
    {
        health = maxHealth;
    }

    public void modifyHealth(float healthModifyValue)
    {
        health += healthModifyValue;

        if (health > maxHealth)
            health = maxHealth;

        if (health < 0)
            health = 0;
    }

    public bool isDead()
    {
        return health <= 0;
    }
}

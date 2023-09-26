using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void damage(float amount);
    public void kill();

    public Health getHealth();

}

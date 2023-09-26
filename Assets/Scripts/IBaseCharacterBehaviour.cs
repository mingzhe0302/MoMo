using System.Collections;
using UnityEngine;

public interface IBaseCharacterBehaviour
{
    void attack();
    void move();
    void rotate();
    void cycleWeapon();
    void damage(float amount);
    void kill();
    void revive();
    Health getHealth();
    IEnumerator reviveWait();
    void moveTarget(Vector2 targetPosition);
}
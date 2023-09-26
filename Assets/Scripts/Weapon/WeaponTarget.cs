using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapon;

public class WeaponTarget : MonoBehaviour
{
    Vector2 location;

    float distanceFromParent;
    Transform parentTransform;

    float rangeRadius;
    private Vector2 inputVec;
    private Vector2 currentPOS;
    private float size;

    //input vec = mousepos for player
    //            player position for enemy targeting
    public void moveTargetToPos(Transform parentTransform, Vector2 inputVec, float rangeRadius, float size)
    {
        this.rangeRadius = rangeRadius; 
        this.inputVec = inputVec;
        this.size = size;
        
        var position = parentTransform.position;
        Vector2 currentPOS = new Vector2(position.x, position.y);
        
        this.currentPOS = currentPOS;

        transform.position = Vector2.ClampMagnitude((inputVec - currentPOS) ,rangeRadius) + currentPOS;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector2.ClampMagnitude((inputVec - currentPOS), rangeRadius) + currentPOS, size);
    }
}

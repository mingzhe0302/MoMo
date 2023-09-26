using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

[RequireComponent(typeof(SpriteRenderer))]
public class DisplayWeapon : MonoBehaviour
{
    private WeaponBaseClass currentWeapon;
    private SpriteRenderer sR;
    private Animator animator;

    private Transform _parentTransform;
    
    // Start is called before the first frame update
    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        _parentTransform = transform.parent;
        animator = this.GetComponent<Animator>();
    }

    public void UpdateWeapon(WeaponBaseClass currentWeapon)
    {
        this.currentWeapon = currentWeapon;
        
        Sprite s = currentWeapon.sprite;
        sR.sprite = s;
        animator.runtimeAnimatorController = currentWeapon.weaponAnimationController;

        // _parentTransform = GetComponentInParent<Transform>();
    }

    private void FixedUpdate()
    {
        rotateWeaponToPos(_parentTransform, transform.position);
    }

    public void rotateWeaponToPos(Transform parentTransform, Vector2 inputVec)
    {
        // if(Input.GetButton("Fire1"))
        //     Debug.Log("_parentTransform pos" + parentTransform.position + " Current pos" + inputVec);
        
        var position = parentTransform.position;
        Vector2 currentPOS = new Vector2(position.x, position.y);

        Vector3 vectorOfParentToTarget = inputVec - currentPOS;
        float angle = Vector3.SignedAngle(Vector3.right, vectorOfParentToTarget, new Vector3(0,0,1));
        transform.eulerAngles = new Vector3(0, 0, angle);

        // transform.eulerAngles = Vector3.Scale(transform.eulerAngles, new Vector3(0, angle, 0));

        // transform.Rotate(Vector3.forward, angle);
    }
    
    static float angle_between_points(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec1.y - vec2.y, vec1.x - vec2.x) * Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

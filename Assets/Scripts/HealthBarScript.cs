using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private int interpolationTimes = 60;
    private int elapsedFrames;
    
    private Transform _healthBarTransform;
    private Health _parentHealth;
    void Start()
    {
        _healthBarTransform = transform.GetChild(0);
        var i = GetComponentInParent(typeof(IDamageable)) as IDamageable;

        if (i != null) _parentHealth = i.getHealth();
    }

    void FixedUpdate()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationTimes;
        
        var localScale = _healthBarTransform.localScale;

        //a to b, 0 - 1 as parameter
        Vector3 interpolatedPos = Vector3.Lerp(localScale,
            new Vector3(((_parentHealth.health / _parentHealth.maxHealth)), localScale.y, localScale.z),
            interpolationRatio);
        
        elapsedFrames = (elapsedFrames + 1) % (interpolationTimes + 1);

        _healthBarTransform.localScale = interpolatedPos;

        // _healthBarTransform.localScale = new Vector3(((_parentHealth.health / _parentHealth.maxHealth)),
        //     localScale.y,
        //     localScale.z);
    }
    
    
}

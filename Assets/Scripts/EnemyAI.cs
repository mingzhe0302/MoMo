using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


//PASS targetPos, aimedPos, currentPos
[Serializable]
public class EnemyAI
{
    public enum MovementBehaviour
    {
        NoMovement,
        Chase,
        PatternNoArgo,
        PatternWithArgo,
    }

    public enum AttackBehaviour
    {
        AlwaysAttack,
        AttackAtDistance,
        DoNotAttack,
        AttackWhenTouch
    }
    
    public MovementBehaviour movementBehaviour;
    public AttackBehaviour attackBehaviour;
    public List<GameObject> waypoints;
    public bool keepDistanceFromTarget;
    public bool returnToOriginalLocationAfterAgro;
    
    private int _waypointIndex;
    private bool _hasArgoed = false;
    public Vector2 movementVector(Vector2 currentPosition, Vector2 aimedPosition, float argoRange, float keepDistance)
    {
        switch ((int)movementBehaviour)
        {
            
            case 0:
                return Vector2.zero;

            case 1:
                if(!this.keepDistanceFromTarget)
                    return chasePointInRange(currentPosition, aimedPosition, argoRange);
                else
                {
                    return chasePointKeepDistance(currentPosition, aimedPosition, argoRange, keepDistance);
                }

            case 2:
                Vector2 waypointPosition = waypoints[_waypointIndex].transform.position;
                
                if((currentPosition-waypointPosition).magnitude < 0.1f)
                    _waypointIndex++;
                if (_waypointIndex >= waypoints.Count)
                    _waypointIndex = 0;

                return (-currentPosition + waypointPosition).normalized;
            
            case 3:
                if(returnToOriginalLocationAfterAgro)
                    if(keepDistanceFromTarget)
                        if(isArgo(currentPosition, aimedPosition, argoRange))
                            return chasePointKeepDistance(currentPosition, aimedPosition, argoRange, keepDistance);
                        else
                            return (isArgo(currentPosition, aimedPosition, argoRange) ?  (-currentPosition + aimedPosition).normalized : waypointCycle(currentPosition, waypoints));
                    else
                        return (isArgo(currentPosition, aimedPosition, argoRange) ?  (-currentPosition + aimedPosition).normalized : waypointCycle(currentPosition, waypoints));
                else
                    if (!_hasArgoed)
                        return (isArgo(currentPosition, aimedPosition, argoRange) ?  (-currentPosition + aimedPosition).normalized : waypointCycle(currentPosition, waypoints));
                    else
                        if (keepDistanceFromTarget)
                            return chasePointKeepDistance(currentPosition, aimedPosition, argoRange, keepDistance);
                        else
                            return chasePointInRange(currentPosition, aimedPosition, argoRange);
        }

        return Vector2.zero;
    }

    //returns if enemy should attack, (if within range, if between range, always)
    public bool attackBool(Vector2 currentPosition, Vector2 aimedPosition, float attackRange, float dontAttackRange)
    {
        float distanceFromCurrentToAimmed = (-currentPosition + aimedPosition).magnitude;
        
        //Debug.Log(distanceFromCurrentToAimmed);
        switch ((int)attackBehaviour)
        {
            case 0:
                return (distanceFromCurrentToAimmed < attackRange);
            
            case 1:
                if (distanceFromCurrentToAimmed < attackRange && distanceFromCurrentToAimmed > dontAttackRange)
                    return true;
                else
                    return false;
            
            case 2:
                return false;
            
            case 3:
                return (distanceFromCurrentToAimmed < 1f);
                    
        }
        return false;
    }

    private Vector2 chasePointKeepDistance(Vector2 currentPosition, Vector2 aimedPosition, float argoRange, float keepDistance)
    {
        if (isArgo(currentPosition, aimedPosition, argoRange))
        {
            if (((-currentPosition + aimedPosition).magnitude) > (keepDistance))
            {
                return (-currentPosition + aimedPosition).normalized;
            }

            else if (((-currentPosition + aimedPosition).magnitude) < (keepDistance))
            {
                return -((-currentPosition + aimedPosition).normalized * keepDistance) +
                       (-currentPosition + aimedPosition);
            }
        }
        
        return Vector2.zero;
    }

    private Vector2 chasePointInRange(Vector2 currentPosition, Vector2 aimedPosition, float argoRange)
    {
        return (isArgo(currentPosition, aimedPosition, argoRange)
            ? (-currentPosition + aimedPosition).normalized
            : Vector2.zero);
    }

    private bool isArgo(Vector2 currentPosition, Vector2 aimedPosition, float argoRange)
    {
        if (((-currentPosition + aimedPosition).magnitude) < argoRange)
        {
            _hasArgoed = true;
            return true;
        }
        return false;
    }
    
    
    private Vector2 waypointCycle(Vector2 currentPosition, List<GameObject> waypointList)
    {
        Vector2 waypointPosition = waypointList[_waypointIndex].transform.position;
                
        if((currentPosition-waypointPosition).magnitude < 0.1f)
            _waypointIndex++;
        if (_waypointIndex >= waypointList.Count)
            _waypointIndex = 0;

        return (-currentPosition + waypointPosition).normalized;
    }
}
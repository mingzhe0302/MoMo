using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

[Serializable]

public class CharacterMovement
{
    public Vector2 maxSpeed;

    [NonSerialized]
    public Rigidbody2D rb;

    public void move(Vector2 inputVec)
    {
        // rb.AddForce(inputVec * maxSpeed, ForceMode.VelocityChange); //unavailable for 2d
        // Vector3 velocityChange = (rb.velocity/maxSpeed);
        // rb.AddForce(velocityChange, ForceMode2D.Impulse);
        //
        // rb.AddForce(new Vector3(-rb.velocity.x, 0) * rb.mass * stopSpeed * Time.deltaTime, ForceMode2D.Impulse);
        
        // Debug.Log(rb.velocity);

        if (inputVec == Vector2.zero)
            rb.velocity.Scale(new Vector2(0.99f,0.99f));
        
        rb.velocity = inputVec * maxSpeed;
        
        // rb.MovePosition(rb.position + inputVec);
    }
    public void rotate_to_point(Vector2 inputVec)
    {
        float a = angle_between_points(rb.position, inputVec);
        rb.MoveRotation(a + 180);
    }
    
    public static float angle_between_points(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec1.y - vec2.y, vec1.x - vec2.x) * Mathf.Rad2Deg;
    }
}

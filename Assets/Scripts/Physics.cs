using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour
{
    [SerializeField] public float mass;
    public Vector3 accelerationApplied;
    [SerializeField] public Vector3 velocity;
    [SerializeField] public float frictionCoefficient;
    [SerializeField] public Vector3 gravity;
    
    
    public void ApplyForce(Vector3 force)
    {
        accelerationApplied += force / mass;
    }

    public void ApplyImpulse(Vector3 impulse)
    {
        velocity += impulse / mass;
    }

    public void ApplyFriction()
    {
        ApplyForce(velocity*-frictionCoefficient);
    }
    
    public void ApplyGravity()
    {
        ApplyForce(gravity);
    }
    
}

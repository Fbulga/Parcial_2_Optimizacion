using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Structs;
using Enums;
using UnityEngine;

public class CustomPhysicsNuestro : MonoBehaviour
{
    [SerializeField] private float mass;
    public Vector3 accelerationApplied;
    [SerializeField] public Vector3 velocity;
    [SerializeField] private float frictionCoefficient;
    
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

    public bool RectangleCollision(Collider rec1, Collider rec2)
    {
        if (rec1.bounds.min.x < rec2.bounds.max.x &&
            rec2.bounds.min.x < rec1.bounds.max.x &&
            rec1.bounds.min.y < rec2.bounds.max.y &&
            rec2.bounds.min.y < rec1.bounds.max.y)
            return true;      

        return false;
    }

    public bool SphereRectangleCollision(Collider rec, SphereCollider sphere)
    {
        Vector2 closestPoint = sphere.transform.position;
        if(closestPoint.x < rec.bounds.min.x) closestPoint.x = rec.bounds.min.x;
        if (closestPoint.x > rec.bounds.max.x) closestPoint.x = rec.bounds.max.x;
        if(closestPoint.y < rec.bounds.min.y) closestPoint.y = rec.bounds.min.y;
        if (closestPoint.y > rec.bounds.max.y) closestPoint.y = rec.bounds.max.y;

        return Vector2.Distance(closestPoint, sphere.transform.position) < sphere.radius;
    }
    public CollisionResponseDto SphereRectangleCollisionStruct(Collider rec, SphereCollider sphere)
    {
        Vector2 closestPoint = sphere.transform.position;
        
        if (closestPoint.x < rec.bounds.min.x) closestPoint.x = rec.bounds.min.x;
        if (closestPoint.x > rec.bounds.max.x) closestPoint.x = rec.bounds.max.x;
        if (closestPoint.y < rec.bounds.min.y) closestPoint.y = rec.bounds.min.y;
        if (closestPoint.y > rec.bounds.max.y) closestPoint.y = rec.bounds.max.y;
        
        bool isTouching = Vector2.Distance(closestPoint, sphere.transform.position) < sphere.radius;

        // Determina el tipo de colisión (horizontal, vertical o esquina)
        CollisionType collisionType = CollisionType.Corner;
        if (isTouching)
        {
            float deltaX = Mathf.Abs(closestPoint.x - sphere.transform.position.x);
            float deltaY = Mathf.Abs(closestPoint.y - sphere.transform.position.y);
            
            if (deltaX > deltaY)
            {
                collisionType = CollisionType.Horizontal;
            }
            else if (deltaY > deltaX)
            {
                collisionType = CollisionType.Vertical;
            }
            else //if (Mathf.Approximately(deltaX, deltaY))
            {
                collisionType = CollisionType.Corner;
            }
        }
        return new CollisionResponseDto() { closestPoint = closestPoint, isTouching = isTouching, collisionType = collisionType };
    }

    public bool SphereCollision(SphereCollider sphere1, SphereCollider sphere2)
    {
        return Vector2.Distance(sphere1.transform.position, sphere2.transform.position) < 
               sphere1.radius * sphere1.transform.lossyScale.x + sphere2.radius * sphere2.transform.lossyScale.x;
    }
    
}

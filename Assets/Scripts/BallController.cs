using System;
using DefaultNamespace;
using Interfaces;
using UnityEngine;

public class BallController : CustomBehaviour, IPhysics, ISphere
{
    private CustomPhysicsNuestro customPhysicsNuestro;

    [SerializeField] private float radius;
    [SerializeField] private float playerInfluenceFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform ballPoint;

    private Collider[] colliders = new Collider[10];
    private bool a = true;

    private SphereCollider sphereCollider;
    public float MaxSpeed => maxSpeed;

    protected override void CustomStart()
    {
        customPhysicsNuestro = gameObject.GetComponent<CustomPhysicsNuestro>();
        sphereCollider = GetComponent<SphereCollider>();
        
    }
    
    protected override void CustomFixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
        if (!GameManager.Instance.ballOnBoard)
        {
            CheckCollisions();
        }
    }

    public void ReUseMe()
    {        
        transform.position = ballPoint.position;
        customPhysicsNuestro.velocity = Vector3.zero;
    }

    public void ImpulseMe()
    {
        transform.SetParent(null);
        customPhysicsNuestro.ApplyImpulse(new Vector3(0,1,0) * maxSpeed);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ActiveBallsDown();
    }
    
    void CheckCollisions()
    {
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                if (collider.TryGetComponent<BoxCollider>(out BoxCollider box))
                {
                    var response = customPhysicsNuestro.SphereRectangleCollisionStruct(box, sphereCollider);
                    if (response.isTouching)
                    {
                        float playerVelocity = 0f;
                        
                        collider.TryGetComponent<DeadZone>(out DeadZone deadZone);
                        if (deadZone != null)
                        {
                            Deactivate();
                            return;
                        }
                        
                        collider.TryGetComponent<PlayerController>(out PlayerController player);
                        collider.TryGetComponent<IDestructible>(out IDestructible brick);
                        
                        if (brick != null) brick.TryDestroyMe();
                        if (player != null) playerVelocity = player.Velocity.magnitude * Mathf.Sign(player.Velocity.x);

                        // Reflejar la velocidad usando la normal en el punto de colisión
                        Vector2 normal = response.collisionNormal;
                        Vector2 reflectedVelocity =
                            Vector2.Reflect(
                                new Vector2(customPhysicsNuestro.velocity.x, customPhysicsNuestro.velocity.y), normal);
                        
                        customPhysicsNuestro.velocity = new Vector3(reflectedVelocity.x, reflectedVelocity.y, 0);
                        
                        transform.position = response.closestPoint + normal * ((sphereCollider.radius) + 0.025f);
                        if (normal.y != 0) // Colisión vertical
                        {
                            Vector2 newVec = new Vector2(
                                customPhysicsNuestro.velocity.x + playerVelocity * playerInfluenceFactor,
                                customPhysicsNuestro.velocity.y
                            );

                            newVec = newVec.normalized * maxSpeed;
                            customPhysicsNuestro.velocity = new Vector3(newVec.x, newVec.y, 0);
                        }
                        
                    }
                }
            }
        }
    }
   
}
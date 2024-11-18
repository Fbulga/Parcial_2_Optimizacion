using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class BallController : MonoBehaviour, IPhysics, ISphere
{
    private CustomPhysicsNuestro customPhysicsNuestro;

    [SerializeField] private float radius;
    [SerializeField] private float playerInfluenceFactor;
    [SerializeField] private float maxSpeed;
    private Collider[] colliders = new Collider[4];

    private SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        customPhysicsNuestro = this.gameObject.GetComponent<CustomPhysicsNuestro>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
        if (!GameManager.Instance.ballOnBoard)
        {
            CheckCollisions();
        }
    }

    void CheckCollisions()
    {
        foreach (var collider in colliders)
        {
            if (collider is not null)
            {
                if (collider.TryGetComponent<BoxCollider>(out BoxCollider box))
                {
                    var response = customPhysicsNuestro.SphereRectangleCollisionStruct(box, sphereCollider);
                    if (response.isTouching)
                    {
                        collider.TryGetComponent<PlayerController>(out PlayerController player);
                        
                        float playerVelocity = 0f;
                        if (player != null) playerVelocity = player.Velocity.magnitude * Mathf.Sign(player.Velocity.x);

                        // Dirección desde el punto de colisión hasta el centro de la esfera
                        Vector2 direction = (new Vector2(transform.position.x, transform.position.y) - response.closestPoint).normalized;

                        // Ajustar la posición de la esfera teniendo en cuenta su radio
                        transform.position = response.closestPoint + direction * ((sphereCollider.radius / 2)+0.175f);
                        switch (response.collisionType)
                        {
                            case CollisionType.Horizontal:
                                customPhysicsNuestro.velocity = new Vector3(-customPhysicsNuestro.velocity.x, customPhysicsNuestro.velocity.y, 0);
                                break;
                            case CollisionType.Vertical:
                                Vector3 newVelocity =
                                    new Vector3(
                                        customPhysicsNuestro.velocity.x + playerVelocity * playerInfluenceFactor,
                                        -customPhysicsNuestro.velocity.y, 0);
                                customPhysicsNuestro.velocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
                                //customPhysicsNuestro.velocity = Vector3.zero;
                                break;
                            case CollisionType.Corner:
                                customPhysicsNuestro.velocity *= -1;
                                break;
                            default:
                                customPhysicsNuestro.velocity *= -1;
                                break;
                        }
                        Debug.Log("pelota toca borde");
                    }
                }
                // if (collider.TryGetComponent<PlayerController>(out PlayerController paddle))
                // {
                //     if (customPhysicsNuestro.SphereRectangleCollision(box, sphereCollider))
                //     {
                //         //Teletransportar pelota
                //         customPhysicsNuestro.velocity = new Vector3(box.gameObject.transform.position.x-transform.position.x,10f,0f);
                //         //customPhysicsNuestro.ApplyImpulse(Vector3.Normalize(new Vector3(box.gameObject.transform.position.x-transform.position.x,1f,0f)));
                //         Debug.Log("pelota toca paleta");
                //     }
                // }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}

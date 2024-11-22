using Interfaces;
using Managers;
using PhysicsOur;
using Scriptables;
using UnityEngine;

namespace Controllers
{
    public class BallController : CustomBehaviour, IPhysics
    {
        private Collider[] colliders = new Collider[10];
    
        [SerializeField] private BallData data;
        [SerializeField] private CustomPhysicsNuestro customPhysicsNuestro;
        [SerializeField] private SphereCollider sphereCollider;
    
        public float MaxSpeed => data.MaxSpeed;
    
        protected override void CustomFixedUpdate()
        {
            Physics.OverlapSphereNonAlloc(transform.position, data.Radius, colliders);
            CheckCollisions();
        }

        public void ReUseMe(Vector3 position)
        {        
            transform.position = position;
            customPhysicsNuestro.velocity = Vector3.zero;
        }

        public void ImpulseMe(float impulse)
        {
            transform.SetParent(null);
            customPhysicsNuestro.velocity = Vector3.zero;
            customPhysicsNuestro.ApplyImpulse(new Vector3(impulse,1,0) * data.MaxSpeed);
        }
        private void Deactivate()
        {
            BallPool.Instance.ReturnBall(this);
            GameManager.Instance.OnBallsDown?.Invoke();
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
                                if (deadZone.IsDeadly)
                                {
                                    Deactivate();
                                    return;
                                }
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
                        
                            customPhysicsNuestro.velocity = new Vector3(reflectedVelocity.x, reflectedVelocity.y, 0).normalized * data.MaxSpeed;
                        
                            transform.position = response.closestPoint + normal * ((sphereCollider.radius) + 0.025f);
                            if (normal.y != 0) // Colisión vertical
                            {
                                Vector2 newVec = new Vector2(
                                    customPhysicsNuestro.velocity.x + playerVelocity * data.PlayerInfluenceFactor,
                                    customPhysicsNuestro.velocity.y
                                );

                                newVec = newVec.normalized * data.MaxSpeed;
                                customPhysicsNuestro.velocity = new Vector3(newVec.x, newVec.y, 0);
                            }
                        }
                    }
                }
            }
        }
   
    }
}
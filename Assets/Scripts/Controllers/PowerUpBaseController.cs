using Enums;
using Managers;
using PhysicsOur;
using Scriptables;
using UnityEngine;

namespace Controllers
{
    public abstract class PowerUpBaseController : CustomBehaviour
    {
        [SerializeField] protected CustomPhysicsNuestro customPhysicsNuestro;
        [SerializeField] protected PowerUpData data;
        [SerializeField] protected SphereCollider sphereCollider;
        [SerializeField] protected PowerUpType type;
        protected Collider[] colliders = new Collider[5];

        protected abstract void UsePowerUp();
        
        protected override void CustomUpdate()
        {
            Physics.OverlapSphereNonAlloc(transform.position, data.Radius, colliders);
        }

        protected override void CustomFixedUpdate()
        {
            CheckCollisions();
        }
        protected void CheckCollisions()
        {
            foreach (var collider in colliders)
            {
                if (collider != null)
                {
                    if (collider.TryGetComponent<BoxCollider>(out BoxCollider box))
                    {
                        if (customPhysicsNuestro.SphereRectangleCollision(box, sphereCollider))
                        {
                            collider.TryGetComponent<DeadZone>(out DeadZone deadZone);
                            if (deadZone != null)
                            {
                                PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
                                return;
                            }
                        
                            collider.TryGetComponent<PlayerController>(out PlayerController player);
                        
                            if (player != null)
                            {
                                UsePowerUp();
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
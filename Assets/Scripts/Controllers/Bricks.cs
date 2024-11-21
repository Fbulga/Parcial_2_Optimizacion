using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bricks: CustomBehaviour, IDestructible
    {
        [SerializeField] private int health = 3;
        private bool destroyed = false;
        public void DestroyMe()
        {
            if (destroyed) return;
            PowerUpPool.Instance.TryDropPowerUp(transform.position);
            GameManager.Instance.OnBrickDestroyed?.Invoke();
            destroyed = true;
            Destroy(gameObject);
        }

        public void TryDestroyMe()
        {
            health--;
            if (health <= 0)
            {
                DestroyMe();
            }
        }

        protected override void CustomStart()
        {
            GameManager.Instance.OnBrickCreated?.Invoke();
        }
    }
}
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class Bricks: CustomBehaviour, IDestructible
    {
        [SerializeField] private int health;
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
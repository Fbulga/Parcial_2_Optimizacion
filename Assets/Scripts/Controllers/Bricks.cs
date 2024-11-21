using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bricks: CustomBehaviour, IDestructible
    {
        [SerializeField] private int health = 3;
        public void DestroyMe()
        {
            PowerUpPool.Instance.TryDropPowerUp(transform.position);
            GameManager.Instance.OnBrickDestroyed?.Invoke();
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
            GameManager.Instance.AddBrick();
        }
    }
}
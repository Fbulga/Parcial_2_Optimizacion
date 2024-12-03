using Interfaces;
using Managers;
using Scriptables;
using UnityEngine;

namespace Controllers
{
    public class Bricks: CustomBehaviour, IDestructible
    {
        [SerializeField] private int health;
        [SerializeField] private BrickData data;
        private bool destroyed = false;
        [SerializeField] private GameObject[] angryOtaku;
        public void DestroyMe()
        {
            if (destroyed) return;
            PowerUpPool.Instance.TryDropPowerUp(transform.position);
            GameManager.Instance.OnBrickDestroyed?.Invoke();
            destroyed = true;
            AudioManager.Instance.PlaySound(data.Clip);
            gameObject.SetActive(false);
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
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
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        public void TryDestroyMe()
        {
            health--;
            switch (health)
            {
                case 2:
                    angryOtaku[0].SetActive(true);
                    break;
                case 1:
                    angryOtaku[1].SetActive(true);
                    break;
            }
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
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextPowerUpUI : MonoBehaviour
    {
        private float time;
        [SerializeField] private float cooldownTime;
        private void OnEnable()
        {
            time = cooldownTime;
            // gameObject.SetActive(true);
        }
        void Update()
        {
            TimerPowerUp();
        }
        void TimerPowerUp()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
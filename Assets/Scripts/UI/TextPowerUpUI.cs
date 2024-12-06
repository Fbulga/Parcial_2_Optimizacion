using TMPro;
using UnityEngine;

namespace UI
{
    public class TextPowerUpUI : MonoBehaviour
    {
        private float time;
        private void OnEnable()
        {
            time = 1;
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
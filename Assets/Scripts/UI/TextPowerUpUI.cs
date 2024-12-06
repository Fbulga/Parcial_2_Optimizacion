using Managers;
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
            CustomUpdateManager.Instance.OnUpdate += CustomUpdate;
        }
        void CustomUpdate()
        {
            TimerPowerUp();
        }
        private void OnDisable()
        {
            CustomUpdateManager.Instance.OnUpdate -= CustomUpdate;
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
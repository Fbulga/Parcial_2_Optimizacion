using System;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class TimerPowerUpUI : MonoBehaviour
    {
        private float time;
        [SerializeField] private Image fill;
        [SerializeField] private float cooldownTime;
        private void OnEnable()
        {
            time = cooldownTime;
            fill.fillAmount = 0;
            CustomUpdateManager.Instance.OnUpdate += CustomUpdate;
        }
        private void OnDisable()
        {
            CustomUpdateManager.Instance.OnUpdate -= CustomUpdate;
        }
        private void CustomUpdate()
        {
            TimerPowerUp();
        }

        void TimerPowerUp()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                fill.fillAmount = (cooldownTime - time) / cooldownTime;
            }
            else
            {
                time = 0;
                fill.fillAmount = 1;
                gameObject.SetActive(false);
            }
        }
    }
}


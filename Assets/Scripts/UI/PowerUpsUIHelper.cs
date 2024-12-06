using System;
using System.Collections.Generic;
using Enums;
using Structs;
using UnityEngine;

namespace UI
{
    public class PowerUpsUIHelper : MonoBehaviour
    {
        [SerializeField] private PowerUpType[] powerUpTypes;
        [SerializeField] private GameObject[] powerUpTexts;
        [SerializeField] private GameObject[] powerUpTimers;

        private Dictionary<PowerUpType, PowerUpUIElements> powerUpDictionary;
        public static Action<PowerUpType, bool> OnPowerUpChanged;

        private void Start()
        {
            OnPowerUpChanged += ActivatePowerUpUI;
            InitializeDic();
        }
        private void OnDestroy()
        {
            OnPowerUpChanged -= ActivatePowerUpUI;
        }

        private void InitializeDic()
        {
            powerUpDictionary = new Dictionary<PowerUpType, PowerUpUIElements>();

            for (int i = 0; i < powerUpTypes.Length; i++)
            {
                GameObject text = powerUpTexts.Length > i ? powerUpTexts[i] : null;
                GameObject timer = powerUpTimers.Length > i ? powerUpTimers[i] : null;
                powerUpDictionary.Add(powerUpTypes[i], new PowerUpUIElements(text, timer));
            }
            //var a = powerUpDictionary;
        }

        // Método para activar los elementos de un PowerUp dado su tipo
        public void ActivatePowerUpUI(PowerUpType powerUpType, bool showTimer = false)
        {
            if (powerUpDictionary.TryGetValue(powerUpType, out PowerUpUIElements elements))
            {
                if (elements.text != null)
                {
                    elements.text.SetActive(false);
                    elements.text.SetActive(true);
                }

                if (showTimer && elements.timer != null)
                {
                    elements.timer.SetActive(false);
                    elements.timer.SetActive(true);
                }
            }
        }

        // // Método para desactivar los elementos de un PowerUp dado su tipo
        // public void DeactivatePowerUpUI(PowerUpType powerUpType)
        // {
        //     if (powerUpDictionary.TryGetValue(powerUpType, out PowerUpUIElements elements))
        //     {
        //         if (elements.text != null)
        //         {
        //             elements.text.SetActive(false);
        //         }
        //
        //         if (elements.timer != null)
        //         {
        //             elements.timer.SetActive(false);
        //         }
        //     }
        // }
    }
}

using Controllers;
using Managers;
using UnityEngine;

namespace PowerUps
{
    public class NoDeathPowerUp : PowerUpBaseController
    {
        protected override void UsePowerUp()
        {
            GameManager.Instance.StartDeadlyTimer();
            PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
            AudioManager.Instance.PlaySound(data.Clip);
            Debug.Log("NoDeathPowerUp");
        }
    }
}
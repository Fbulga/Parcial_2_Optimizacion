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
            Debug.Log("NoDeathPowerUp");
        }
    }
}
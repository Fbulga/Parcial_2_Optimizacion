using Controllers;
using Managers;
using UI;
using UnityEngine;

namespace PowerUps
{
    public class SlowerBallsPowerUp : PowerUpBaseController
    {
        protected override void UsePowerUp()
        {
            GameManager.Instance.ChangeBallsMaxSpeed();
            PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
            AudioManager.Instance.PlaySound(data.Clip);
            PowerUpsUIHelper.OnPowerUpChanged?.Invoke(type,true);
            Debug.Log("SlowerBallsPowerUp");
        }
    }
}
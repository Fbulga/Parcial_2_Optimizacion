using Controllers;
using Managers;
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
        }
    }
}
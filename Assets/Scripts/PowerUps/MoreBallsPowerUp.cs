using Controllers;
using Managers;
using UI;
using UnityEngine;

namespace PowerUps
{
    public class MoreBallsPowerUp : PowerUpBaseController
    {
        protected override void UsePowerUp()
        {
            var ball = BallPool.Instance.GetBall();
            ball.ImpulseMe(0.5f);
            ball = BallPool.Instance.GetBall();
            ball.ImpulseMe(-0.5f);
            PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
            AudioManager.Instance.PlaySound(data.Clip);
            PowerUpsUIHelper.OnPowerUpChanged?.Invoke(type,false);
        }
    }
}
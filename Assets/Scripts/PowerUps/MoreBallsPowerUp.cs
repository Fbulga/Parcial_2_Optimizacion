using DefaultNamespace.Enums;
using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class MoreBallsPowerUp : MonoBehaviour, IPowerUp
    {
        public PowerUpType type { get; set; }

        public void UsePowerUp()
        {
            var ball = BallPool.Instance.GetBall();
            ball.ImpulseMe();
            PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
            //gameObject.SetActive(false);
        }
    }
}
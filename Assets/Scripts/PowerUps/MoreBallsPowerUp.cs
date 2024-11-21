using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class MoreBallsPowerUp : MonoBehaviour, IPowerUp

    {
        public void UsePowerUp()
        {
            var ball = BallPool.Instance.GetBall();
            ball.ImpulseMe();
            gameObject.SetActive(false);
        }
    }
}
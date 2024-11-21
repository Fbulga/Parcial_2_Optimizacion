namespace DefaultNamespace
{
    public class MoreBallsPowerUp : PowerUpBaseController
    {
        protected override void UsePowerUp()
        {
            var ball = BallPool.Instance.GetBall();
            ball.ImpulseMe();
            PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
        }
    }
}
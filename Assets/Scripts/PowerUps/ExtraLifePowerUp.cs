using Controllers;
using Managers;
using UnityEngine;

namespace PowerUps
{
    public class ExtraLifePowerUp : PowerUpBaseController
    {
        protected override void UsePowerUp()
        {
            GameManager.Instance.OnHealthUp?.Invoke();
            PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
            AudioManager.Instance.PlaySound(data.Clip);
            Debug.Log("ExtraLifePowerUp");
        }
    }
}
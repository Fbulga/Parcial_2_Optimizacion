using Controllers;
using Managers;
using UI;
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
            PowerUpsUIHelper.OnPowerUpChanged?.Invoke(type,false);
            Debug.Log("ExtraLifePowerUp");
        }
    }
}
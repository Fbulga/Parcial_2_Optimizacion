using Controllers;
using Managers;
using UI;
using UnityEngine;

namespace PowerUps
{
    public class LongerBarPowerUp : PowerUpBaseController
    {
        protected override void UsePowerUp()
        {
            GameManager.Instance.SetLongerPLayer();
            PowerUpPool.Instance.ReturnPowerUp(gameObject, type);
            AudioManager.Instance.PlaySound(data.Clip);
            PowerUpsUIHelper.OnPowerUpChanged?.Invoke(type,true);
        }
    }
}
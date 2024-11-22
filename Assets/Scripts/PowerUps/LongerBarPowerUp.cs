using Controllers;
using Managers;
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
            Debug.Log("LongerBarPowerUp");
        }
    }
}
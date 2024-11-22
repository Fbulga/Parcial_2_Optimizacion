using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthAndPoints : CustomBehaviour
    {
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private TextMeshProUGUI healthText;

        protected override void CustomStart()
        {
            GameManager.Instance.SetPointsAndHealthUI(this);
        }

        public void UpdateHealth(int health)
        {
            healthText.text = health.ToString();
        }
        public void UpdatePoints(int points)
        {
            pointsText.text = points.ToString();
        }
        
    }
}
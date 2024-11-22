using Managers;
using TMPro;
using UnityEngine;

public class EndGamePoints : CustomBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    protected override void CustomStart()
    {
        pointsText.text = GameManager.Instance.points.ToString();
    }
}

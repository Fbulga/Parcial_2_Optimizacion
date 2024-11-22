using System;
using Managers;
using TMPro;
using UnityEngine;

public class EndGamePoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    void Start()
    {
        pointsText.text = GameManager.Instance.points.ToString();
    }
}

using UnityEngine;

public class CanvasCredits : MonoBehaviour
{
    public Canvas creditsCanvas;

    public void ShowCredits()
    {
        creditsCanvas.enabled = true;
    }

    public void HideCredits()
    {
        creditsCanvas.enabled = false;
    }
}

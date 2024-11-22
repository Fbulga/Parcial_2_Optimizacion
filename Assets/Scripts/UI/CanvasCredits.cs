using UnityEngine;

public class CanvasCredits : MonoBehaviour
{
    public Canvas creditsCanvas;
    public Canvas buttonsCanvas;

    public void ShowCredits()
    {
        creditsCanvas.enabled = true;
        buttonsCanvas.enabled = false;
    }

    public void HideCredits()
    {
        creditsCanvas.enabled = false;
        buttonsCanvas.enabled = true;
    }
}
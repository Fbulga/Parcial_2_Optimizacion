using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ExitButton : MonoBehaviour
    {
        public void GoToMenu()
        {
            GameManager.Instance.OnRestart?.Invoke();
            SceneManager.LoadScene(0);
        }
    }
}
using Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public void GoToGame()
        {
            SceneManager.LoadScene(1);
            GameManager.Instance.OnRestart?.Invoke();
        }

        public void ExitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GoToGame();
            }
        }
    }
}
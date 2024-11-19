using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.UI
{
    public class Menu : MonoBehaviour
    {
        public void GoToGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
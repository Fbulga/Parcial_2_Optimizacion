using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GoToMenuButton : CustomBehaviour
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }

        protected override void CustomUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
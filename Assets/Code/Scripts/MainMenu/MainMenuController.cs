using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.MainMenu
{
    public class MainMenuController
    {
        public void StartGame()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
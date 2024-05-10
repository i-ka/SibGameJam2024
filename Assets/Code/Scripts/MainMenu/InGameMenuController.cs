using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Scripts.MainMenu
{
    public class InGameMenuController: ITickable
    {
        private bool _isGameMenuShown;
        public bool IsGameMenuShown
        {
            get => _isGameMenuShown;
            set
            {
                if (value == _isGameMenuShown)
                    return;
                
                if (value)
                {
                    GameMenuShown?.Invoke();
                    _isGameMenuShown = true;
                }
                else
                {
                    GameMenuHidden?.Invoke();
                    _isGameMenuShown = false;
                }
            }
        }

        public event Action GameMenuShown;
        public event Action GameMenuHidden;
        
        public void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        public void Tick()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                IsGameMenuShown = !IsGameMenuShown;
            }
        }
    }
}

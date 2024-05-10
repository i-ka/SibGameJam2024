using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Scripts.MainMenu
{
    public class InGameMenuController: ITickable
    {
        private readonly GameFlowController _gameFlowController;
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
                    Cursor.visible = true;
                    _isGameMenuShown = true;
                    _gameFlowController.PauseGame();
                }
                else
                {
                    GameMenuHidden?.Invoke();
                    Cursor.visible = false;
                    _isGameMenuShown = false;
                    _gameFlowController.UnpauseGame();
                }
            }
        }
        
        public InGameMenuController(GameFlowController gameFlowController)
        {
            _gameFlowController = gameFlowController;
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

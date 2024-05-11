using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Scripts.MainMenu
{
    public class InGameMenuController: ITickable
    {
        private readonly GameStateController _gameStateController;
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
                }
                else
                {
                    GameMenuHidden?.Invoke();
                    Cursor.visible = false;
                    _isGameMenuShown = false;
                }
            }
        }

        public event Action GameMenuShown;
        public event Action GameMenuHidden;

        public InGameMenuController(GameStateController gameStateController)
        {
            _gameStateController = gameStateController;
        }
        
        public void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        public void Tick()
        {
            if (_gameStateController.IsOver) return;
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                IsGameMenuShown = !IsGameMenuShown;
            }
        }
    }
}

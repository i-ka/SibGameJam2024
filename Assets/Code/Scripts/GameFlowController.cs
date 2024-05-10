using System;
using UnityEngine;

namespace Code.Scripts
{
    public class GameFlowController
    {
        public event Action OnGameOver;

        public void GameOver()
        {
            OnGameOver?.Invoke();
            PauseGame();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
        }
    }
}
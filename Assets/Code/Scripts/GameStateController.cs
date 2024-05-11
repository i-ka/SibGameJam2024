using System;
using UnityEngine;

namespace Code.Scripts
{
    public class GameStateController
    {
        public event Action GameOver;
        public event Action GameFinished;
        
        private readonly GameGoalSettings _goalSettings;
        private int destroyedHives = 0;
        public bool IsOver { get; private set; } = false;

        private GameStateController(GameGoalSettings goalSettings)
        {
            _goalSettings = goalSettings;
        }

        public void PlayerDied()
        {
            IsOver = true;
            GameOver?.Invoke();
            Debug.Log("Player died. Game over");
        }

        public void HiveDestroy()
        {
            destroyedHives++;
            if (destroyedHives >= _goalSettings.CountHivesToDestroy)
            {
                IsOver = true;
                GameFinished?.Invoke();
                Debug.Log("Yapeee!!! Player finished the game");
            }
        }
    }
}

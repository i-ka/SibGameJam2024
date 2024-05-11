using System;
using UnityEngine;

namespace Code.Scripts
{
    public class GameStateController
    {
        public event Action GameOver;
        public event Action GameFinished;
        
        private readonly GameGoalSettings _goalSettings;
        
        public bool IsOver { get; private set; } = false;
        public bool IsWin { get; private set; }
        public int DestroyedHives { get; private set; }
        public int DestroyedEnemies { get; private set; }

        private GameStateController(GameGoalSettings goalSettings)
        {
            _goalSettings = goalSettings;
        }

        public void PlayerDied()
        {
            IsOver = true;
            IsWin = false;
            GameOver?.Invoke();
            Debug.Log("Player died. Game over");
        }

        public void HiveDestroy()
        {
            DestroyedHives++;
            if (DestroyedHives >= _goalSettings.CountHivesToDestroy)
            {
                IsOver = true;
                IsWin = true;
                GameFinished?.Invoke();
                Debug.Log("Yapeee!!! Player finished the game");
            }
        }

        public void EnemyKilled()
        {
            DestroyedEnemies++;
        }
    }
}

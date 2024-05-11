using System;

namespace Code.Scripts
{
    public class GameStateController
    {
        public event Action GameOver;
        public event Action GameFinished;
        
        private readonly GameGoalSettings _goalSettings;
        private int destroyedHives = 0;

        private GameStateController(GameGoalSettings goalSettings)
        {
            _goalSettings = goalSettings;
        }

        public void PlayerDied()
        {
            GameOver?.Invoke();
        }

        public void HiveDestroy()
        {
            destroyedHives++;
            if (destroyedHives >= _goalSettings.CountHivesToDestroy)
            {
                GameFinished?.Invoke();
            }
        }
    }
}

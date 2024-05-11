using UnityEngine;

namespace Code.Scripts
{
    [CreateAssetMenu(menuName = "My Assets/GoalSettings")]
    public class GameGoalSettings : ScriptableObject
    {
        public int CountHivesToDestroy;
    }
}
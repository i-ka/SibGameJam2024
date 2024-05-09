using UnityEngine;

namespace Code.Scripts.StateMachine
{
    public class StateMachineRunner : MonoBehaviour
    {
        public IStateMachine StateMachine { get; set; }

        private void Update()
        {
            StateMachine?.Tick();
        }
    }
}
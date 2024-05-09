using UnityEngine;

namespace Code.Scripts.StateMachine
{
    public class StateMachineRunner : MonoBehaviour
    {
        public StateMachine StateMachine { get; set; }

        private void Update()
        {
            if (StateMachine != null)
            {
                StateMachine.Tick();
            }
        }
    }
}
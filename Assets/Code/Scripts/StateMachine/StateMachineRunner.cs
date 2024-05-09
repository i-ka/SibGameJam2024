using System;
using UnityEngine;

namespace StateMachine
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
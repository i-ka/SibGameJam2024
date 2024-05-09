using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class WaitNode: IBtNode
    {
        private readonly float _timeSeconds;

        private float _currentTime;

        public WaitNode(float timeSeconds)
        {
            _timeSeconds = timeSeconds;
        }

        public void OnEnter()
        {
            _currentTime = 0;
        }

        public BtNodeResult Tick()
        {
            if (_currentTime >= _timeSeconds)
            {
                return BtNodeResult.Success();
            }

            _currentTime += Time.deltaTime;
            return BtNodeResult.Running();
        }

        public void OnExit()
        {
        }
    }
}
using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class WaitNode: IBtNode
    {
        private readonly float _timeSeconds;
        private readonly string _tag;

        private float _currentTime;

        public WaitNode(float timeSeconds, string tag = null)
        {
            _timeSeconds = timeSeconds;
            _tag = tag;
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
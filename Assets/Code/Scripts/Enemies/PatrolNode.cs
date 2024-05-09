using System;
using Code.Scripts.StateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Enemy
{
    public class PatrolNode: IBtNode
    {
        private readonly EnemyBTContext _context;
        private readonly float _angerDistance;
        private int _currentPathIndex;
        private bool _isStarted;
        private Vector3? _targetPosition = null;

        public PatrolNode(EnemyBTContext context, float angerDistance)
        {
            _context = context;
            _angerDistance = angerDistance;
        }
        
        public void OnEnter()
        {
            _currentPathIndex = 0;
            _isStarted = false;
            _targetPosition = null;
            Debug.Log("Start patrol");
        }

        public BtNodeResult Tick()
        {
            var vectorToPlayer = _context.Player.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            var directionToPlayer = vectorToPlayer.normalized;
            if (sqrPlayerDistance <= Mathf.Pow(_angerDistance, 2))
            {
                Debug.DrawRay(_context.Self.transform.position, directionToPlayer, Color.green);
                var hit = Physics.Raycast(_context.Self.transform.position, directionToPlayer, out var hitInfo);
                if (hit && hitInfo.transform == _context.Player)
                {
                    _context.DetectedPlayer = hitInfo.transform;
                    return BtNodeResult.Success();
                }
            }

            var hivePosition = _context.Hive.HivePosition.position;
            if (_targetPosition == null)
            {
                _targetPosition = hivePosition + Random.insideUnitSphere * 10;
                _context.NavMeshAgent.SetDestination(_targetPosition.Value);
            }
            else if (_context.NavMeshAgent.remainingDistance <= 0.2f)
            {
                _targetPosition = null;
            }

            return BtNodeResult.Running();
        }
            

        public void OnExit()
        {
            Debug.Log("Stop patrol");
        }
    }
}
using System;
using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class PatrolNode: IBtNode
    {
        private readonly EnemyBTContext _context;
        private readonly float _angerDistance;
        private int _currentPathIndex;
        private bool _isStarted;

        public PatrolNode(EnemyBTContext context, float angerDistance)
        {
            _context = context;
            _angerDistance = angerDistance;
        }
        
        public void OnEnter()
        {
            _currentPathIndex = 0;
            _isStarted = false;
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

            var path = _context.Hive.PatrolPath;
            if (path?.Length > 0)
            {
                if (!_isStarted)
                {
                    _context.NavMeshAgent.SetDestination(path[_currentPathIndex].position);
                    _isStarted = true;
                }
                if (_context.NavMeshAgent.remainingDistance <= 0.2f)
                {
                    _currentPathIndex++;
                    if (_currentPathIndex >= path.Length)
                        _currentPathIndex = 0;
                    _context.NavMeshAgent.SetDestination(path[_currentPathIndex].position);
                }
            }

            return BtNodeResult.Running();
        }
            

        public void OnExit()
        {
            Debug.Log("Stop patrol");
        }
    }
}
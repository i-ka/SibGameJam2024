using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class ChaseNode: IBtNode
    {
        private readonly EnemyBTContext _context;
        private readonly float _angerDistance;
        private readonly float _attackDistance;

        public ChaseNode(EnemyBTContext context, float angerDistance, float attackDistance)
        {
            _context = context;
            _angerDistance = angerDistance;
            _attackDistance = attackDistance;
        }
        
        public void OnEnter()
        {
            Debug.Log("Start chase");
        }

        public BtNodeResult Tick()
        {
            if (!_context.DetectedPlayer)
                return BtNodeResult.Failure();
            
            var vectorToPlayer = _context.DetectedPlayer.transform.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            if (sqrPlayerDistance > Mathf.Pow(_angerDistance, 2))
            {
                _context.DetectedPlayer = null;
                return BtNodeResult.Failure();
            }

            _context.NavMeshAgent.SetDestination(_context.DetectedPlayer.transform.position);
            
            if (sqrPlayerDistance < Mathf.Pow(_attackDistance, 2))
            {
                return BtNodeResult.Success();
            }

            return BtNodeResult.Running();
        }

        public void OnExit()
        {
            Debug.Log("Stop chase");
        }
    }
}
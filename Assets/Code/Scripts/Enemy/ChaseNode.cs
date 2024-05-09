using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class ChaseNode: IBtNode
    {
        private readonly EnemyBTContext _context;

        public ChaseNode(EnemyBTContext context)
        {
            _context = context;
        }
        
        public void OnEnter()
        {
            Debug.Log("Start chase");
        }

        public BtNodeResult Tick()
        {
            var vectorToPlayer = _context.Player.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            if (sqrPlayerDistance > Mathf.Pow(_context.Self.angerDistance, 2))
            {
                _context.DetectedPlayer = null;
                return BtNodeResult.Failure();
            }

            _context.NavMeshAgent.SetDestination(_context.Player.position);
            
            if (sqrPlayerDistance < Mathf.Pow(_context.Self.attackDistance, 2))
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
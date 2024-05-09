using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class AttackNode: IBtNode
    {
        private readonly EnemyBTContext _context;
        private readonly float _attackDistance;

        public AttackNode(EnemyBTContext context, float attackDistance)
        {
            _context = context;
            _attackDistance = attackDistance;
        }
        
        public void OnEnter()
        {
            Debug.Log("Perform attack");
        }

        public BtNodeResult Tick()
        {
            if (!_context.DetectedPlayer)
                return BtNodeResult.Failure();
            
            var vectorToPlayer = _context.DetectedPlayer.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            if (sqrPlayerDistance > Mathf.Pow(_attackDistance, 2))
            {
                return BtNodeResult.Failure();
            }
            Debug.Log("Attack player");
            return BtNodeResult.Success();
        }

        public void OnExit()
        {
            Debug.Log("Stop attack");
        }
    }
}
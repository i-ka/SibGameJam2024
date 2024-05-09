using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class AttackNode: IBtNode
    {
        private readonly EnemyBTContext _context;

        public AttackNode(EnemyBTContext context)
        {
            _context = context;
        }
        
        public void OnEnter()
        {
        }

        public BtNodeResult Tick()
        {
            var vectorToPlayer = _context.Player.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            var directionToPlayer = vectorToPlayer.normalized;
            if (sqrPlayerDistance > 4)
            {
                return BtNodeResult.Failure();
            }
            Debug.Log("Attack player");
            return BtNodeResult.Success();
        }

        public void OnExit()
        {
        }
    }
}
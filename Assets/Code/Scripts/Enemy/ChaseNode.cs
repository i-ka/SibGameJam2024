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
        }

        public BtNodeResult Tick()
        {
            var vectorToPlayer = _context.Player.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            var directionToPlayer = vectorToPlayer.normalized;
            if (sqrPlayerDistance > Mathf.Pow(_context.Self.angerDistance, 2))
            {
                return BtNodeResult.Failure();
            }
            
            _context.Self.transform.Translate(directionToPlayer * (10 * Time.deltaTime));
            if (sqrPlayerDistance < Mathf.Pow(_context.Self.attackDistance, 2))
            {
                return BtNodeResult.Success();
            }

            return BtNodeResult.Running();
        }

        public void OnExit()
        {
        }
    }
}
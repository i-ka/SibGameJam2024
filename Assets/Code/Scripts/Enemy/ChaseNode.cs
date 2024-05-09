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
            if (sqrPlayerDistance > 9)
            {
                return BtNodeResult.Failure();
            }
            
            _context.Self.transform.Translate(directionToPlayer * (10 * Time.deltaTime));
            if (sqrPlayerDistance < 4)
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
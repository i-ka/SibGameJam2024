using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class ChaseNode: IBtNode
    {
        private readonly EnemyBTContext _context;
        private readonly float _angerDistance;
        private readonly float _attackDistance;
        
        private static readonly int AngryPropertyId = Animator.StringToHash("Agro");
        

        public ChaseNode(EnemyBTContext context, float angerDistance, float attackDistance)
        {
            _context = context;
            _angerDistance = angerDistance;
            _attackDistance = attackDistance;
        }
        
        public void OnEnter()
        {
            //_context.Animator.SetTrigger(Awake);
            _context.Animator.SetFloat(AngryPropertyId, 1);
        }

        public BtNodeResult Tick()
        {
            var player = _context.Hive.DetectedPlayer ?? _context.DetectedPlayer;
            if (!_context.DetectedPlayer)
                return BtNodeResult.Failure();
            
            var vectorToPlayer = player.transform.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            if (sqrPlayerDistance > Mathf.Pow(_angerDistance, 2))
            {
                _context.DetectedPlayer = null;
                _context.Animator.SetFloat(AngryPropertyId, 0);
                return BtNodeResult.Failure();
            }

            _context.NavMeshAgent.SetDestination(player.transform.position);
            
            if (sqrPlayerDistance < Mathf.Pow(_attackDistance, 2))
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
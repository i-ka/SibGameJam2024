using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Enemy
{
    public class AttackNode: IBtNode
    {
        private readonly EnemyBTContext _context;
        private readonly float _damage;
        private readonly float _attackDistance;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public AttackNode(EnemyBTContext context, float damage, float attackDistance)
        {
            _context = context;
            _damage = damage;
            _attackDistance = attackDistance;
        }
        
        public void OnEnter()
        {
            _context.Animator.SetTrigger(Attack);
            
        }

        public BtNodeResult Tick()
        {
            if (!_context.DetectedPlayer)
                return BtNodeResult.Failure();
            
            var vectorToPlayer = _context.DetectedPlayer.position - _context.Self.transform.position;
            var sqrPlayerDistance = vectorToPlayer.sqrMagnitude;
            if (sqrPlayerDistance > Mathf.Pow(_attackDistance, 2))
            {
                _context.Animator.ResetTrigger(Attack);
                return BtNodeResult.Failure();
            }

            var canDamage = _context.DetectedPlayer.TryGetComponent<HealthComponent>(out var healthComponent);
            if (!canDamage)
            {
                _context.Animator.ResetTrigger(Attack);
                return BtNodeResult.Failure();
            }
            
            healthComponent.ApplyDamage(_damage);
            return BtNodeResult.Success();
        }

        public void OnExit()
        {
        }
    }
}
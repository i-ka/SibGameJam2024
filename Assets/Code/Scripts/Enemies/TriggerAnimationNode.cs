using Code.Scripts.StateMachine;

namespace Code.Scripts.Enemy
{
    public class TriggerAnimationNode: IBtNode
    {
        private int _animationId;
        private EnemyBTContext _context;
        
        public TriggerAnimationNode(EnemyBTContext context, int animationId)
        {
            _animationId = animationId;
            _context = context;
        }
        
        public void OnEnter()
        {
        }

        public BtNodeResult Tick()
        {
            _context.Animator.SetTrigger(_animationId);
            return BtNodeResult.Success();
        }

        public void OnExit()
        {
        }
    }
}
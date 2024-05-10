using Code.Scripts.StateMachine;

namespace Code.Scripts.Enemy
{
    public class PropagateDetectedPlayerToHive: IBtNode
    {
        private readonly EnemyBTContext _context;

        public PropagateDetectedPlayerToHive(EnemyBTContext context)
        {
            _context = context;
        }

        public void OnEnter()
        {
        }

        public BtNodeResult Tick()
        {
            if (_context.Hive == null)
                return BtNodeResult.Success();
            
            _context.Hive.DetectedPlayer = _context.DetectedPlayer;
            return BtNodeResult.Success();
        }

        public void OnExit()
        {
        }
    }
}
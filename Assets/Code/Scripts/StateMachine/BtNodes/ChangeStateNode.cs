namespace Code.Scripts.StateMachine.BtNodes
{
    public class ChangeStateNode: IBtNode
    {
        private readonly IState _targetState;

        public ChangeStateNode(IState targetState)
        {
            _targetState = targetState;
        }
        
        public void OnEnter() { }

        public BtNodeResult Tick() => BtNodeResult.ChangeState(_targetState);

        public void OnExit() { }
    }
}
namespace StateMachine
{
    public class RunBtState : IState
    {
        private readonly IBtNode _node;

        public RunBtState(IBtNode node)
        {
            _node = node;
        }

        public void OnEnter()
        {
            _node.OnEnter();
        }

        public StateResult Tick()
        {
            var nodeResult = _node.Tick();

            if (nodeResult.Type == BtResultType.StateTransition)
                return StateResult.ChangeState(nodeResult.TargetState);

            return StateResult.Running();
        }

        public void OnExit()
        {
            _node.OnExit();
        }
    }
}
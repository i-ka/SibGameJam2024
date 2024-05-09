namespace Code.Scripts.StateMachine
{
    public class RunBtState<T> : IState<T>
    {
        private readonly IBtNode _node;
        private BtNodeResult _lastResult;

        public RunBtState(IBtNode node, T blackBoard)
        {
            _node = node;
            BlackBoard = blackBoard;
        }

        public void OnEnter()
        {
            _lastResult = default;
            _node.OnEnter();
        }

        public void Tick()
        {
            if (_lastResult.Type is BtResultType.Success or BtResultType.Failure)
                return;
            
            var result = _node.Tick();
            
            if (result.Type is BtResultType.Success or BtResultType.Failure)
                _node.OnExit();
            
            _lastResult = result;
        }

        public void OnExit()
        {
            if (_lastResult.Type is BtResultType.Success or BtResultType.Failure)
                return;
            _node.OnExit();
        }

        public T BlackBoard { get; }
    }
}
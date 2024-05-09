namespace Code.Scripts.StateMachine
{
    public class RunBtState<T> : IState<T>
    {
        private readonly IBtNode _node;

        public RunBtState(IBtNode node, T blackBoard)
        {
            _node = node;
            BlackBoard = blackBoard;
        }

        public void OnEnter()
        {
            _node.OnEnter();
        }

        public void Tick()
        {
            _node.Tick();
        }

        public void OnExit()
        {
            _node.OnExit();
        }

        public T BlackBoard { get; }
    }
}
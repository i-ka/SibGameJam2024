namespace StateMachine
{
    public class StateMachine
    {
        private readonly IState _initialState;
        private IState _currentState;

        public StateMachine(IState initialState)
        {
            _initialState = initialState;
        }

        public void Tick()
        {
            if (_currentState is null)
                EnterState(_initialState);

            var result = _currentState.Tick();

            switch (result.Type)
            {
                case StateResultType.Transition:
                    EnterState(result.TargetState);
                    break;
                case StateResultType.Running:
                default:
                    break;
            }
        }

        public void SetState(IState state)
        {
            EnterState(state);
        }

        private void EnterState(IState targetState)
        {
            _currentState?.OnExit();
            targetState.OnEnter();
            _currentState = targetState;
        }
    }
}
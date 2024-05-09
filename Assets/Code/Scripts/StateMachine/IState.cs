namespace StateMachine
{
    public interface IState
    {
        void OnEnter();
        StateResult Tick();
        void OnExit();
    }
}
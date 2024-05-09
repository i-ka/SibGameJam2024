namespace Code.Scripts.StateMachine
{
    public interface IState
    {
        void OnEnter();
        StateResult Tick();
        void OnExit();
    }
}
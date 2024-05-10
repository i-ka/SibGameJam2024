namespace Code.Scripts.StateMachine
{
    
    public interface IState
    {
        void OnEnter();
        void Tick();
        void OnExit();
    }
    
    public interface IState<TBlackBoard>: IState
    {
        TBlackBoard BlackBoard { get; }
    }
}
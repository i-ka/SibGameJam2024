namespace StateMachine
{
    public interface IBtNode
    {
        void OnEnter();
        BtNodeResult Tick();
        void OnExit();
    }
}
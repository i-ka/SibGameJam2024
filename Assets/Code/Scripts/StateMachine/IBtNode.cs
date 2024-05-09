namespace Code.Scripts.StateMachine
{
    public interface IBtNode
    {
        void OnEnter();
        BtNodeResult Tick();
        void OnExit();
    }
}
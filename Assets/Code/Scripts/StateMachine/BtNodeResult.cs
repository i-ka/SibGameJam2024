using JetBrains.Annotations;

namespace StateMachine
{
    public struct BtNodeResult
    {
        public BtResultType Type { get; }
        [CanBeNull] public IState TargetState { get; }

        private BtNodeResult(BtResultType type, [CanBeNull] IState targetState)
        {
            Type = type;
            TargetState = targetState;
        }

        public static BtNodeResult Running() => new BtNodeResult(BtResultType.Running, null);
        public static BtNodeResult Success() => new BtNodeResult(BtResultType.Success, null);
        public static BtNodeResult Failure() => new BtNodeResult(BtResultType.Failure, null);

        public static BtNodeResult ChangeState(IState targetState) =>
            new BtNodeResult(BtResultType.StateTransition, targetState);
    }
}
using JetBrains.Annotations;

namespace Code.Scripts.StateMachine
{
    public struct StateResult
    {
        public StateResultType Type { get; }
        [CanBeNull] public IState TargetState { get; }

        private StateResult(StateResultType type, [CanBeNull] IState targetState)
        {
            Type = type;
            TargetState = targetState;
        }

        public static StateResult Running() => new StateResult(StateResultType.Running, null);

        public static StateResult ChangeState(IState targetState) =>
            new StateResult(StateResultType.Transition, targetState);
    }
}
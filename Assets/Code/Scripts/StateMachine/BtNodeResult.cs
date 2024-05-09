using JetBrains.Annotations;

namespace Code.Scripts.StateMachine
{
    public struct BtNodeResult
    {
        public BtResultType Type { get; }

        private BtNodeResult(BtResultType type)
        {
            Type = type;
        }

        public static BtNodeResult Running() => new BtNodeResult(BtResultType.Running);
        public static BtNodeResult Success() => new BtNodeResult(BtResultType.Success);
        public static BtNodeResult Failure() => new BtNodeResult(BtResultType.Failure);

    }
}
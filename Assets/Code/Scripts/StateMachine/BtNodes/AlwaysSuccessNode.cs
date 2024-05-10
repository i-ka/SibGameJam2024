using System;

namespace Code.Scripts.StateMachine.BtNodes
{
    public class AlwaysSuccessNode: IBtNode
    {
        private IBtNode _inner;

        public AlwaysSuccessNode(IBtNode inner)
        {
            _inner = inner;
        }

        public void OnEnter()
        {
            _inner.OnEnter();
        }

        public BtNodeResult Tick()
        {
            var result = _inner.Tick();
            switch (result.Type)
            {
                case BtResultType.Success:
                case BtResultType.Failure:
                    return BtNodeResult.Success();
                case BtResultType.Running:
                    return BtNodeResult.Running();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnExit()
        {
            _inner.OnExit();
        }
    }
}
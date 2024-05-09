using System;

namespace Code.Scripts.StateMachine.BtNodes
{
    public class RepeatNode: IBtNode
    {
        private readonly IBtNode _node;
        private readonly int _count;
        private int _currentCount = 0;

        public RepeatNode(IBtNode node, int count = -1)
        {
            if (count == 0)
                throw new ArgumentException("Repeat count cannot be null", nameof(count));
            _node = node;
            _count = count;
        }
        
        public void OnEnter()
        {
            _node.OnEnter();
        }

        public BtNodeResult Tick()
        {
            var result = _node.Tick();
            switch (result.Type)
            {
                case BtResultType.Failure:
                    _node.OnExit();
                    return BtNodeResult.Failure();
                case BtResultType.Success:
                {
                    if (_count > 0)
                    {
                        if (_currentCount == _count)
                        {
                            return BtNodeResult.Success();
                        }
                    }
                    _node.OnExit();
                    _node.OnEnter();
                    _currentCount++;
                    return BtNodeResult.Running();
                }
                case BtResultType.StateTransition:
                    _node.OnExit();
                    return BtNodeResult.ChangeState(result.TargetState);
                case BtResultType.Running:
                case BtResultType.NotRun:
                    return BtNodeResult.Running();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnExit()
        {
            _node.OnExit();
        }
    }
}
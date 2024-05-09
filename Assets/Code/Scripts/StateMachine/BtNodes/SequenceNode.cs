using System;

namespace Code.Scripts.StateMachine.BtNodes
{
    public class SequenceNode: IBtNode
    {
        private readonly IBtNode[] _nodes;
        private IBtNode CurrentNode => _nodes[_currentNodeIndex];
        private int _currentNodeIndex = 0;

        public SequenceNode(params IBtNode[] nodes)
        {
            if (nodes is null || nodes.Length == 0)
                throw new Exception("Child nodes have no items");
            
            _nodes = nodes;
        }

        public void OnEnter()
        {
            _currentNodeIndex = 0;
            CurrentNode.OnEnter();
        }

        public BtNodeResult Tick()
        {
            var currentNodeResult = CurrentNode.Tick();
            switch (currentNodeResult.Type)
            {
                case BtResultType.Success:
                {
                    CurrentNode.OnExit();
                    _currentNodeIndex++;
                    if (_currentNodeIndex == _nodes.Length)
                        return BtNodeResult.Success();
                    CurrentNode.OnEnter();
                    return BtNodeResult.Running();
                }
                case BtResultType.Failure:
                {
                    CurrentNode.OnExit();
                    return BtNodeResult.Failure();
                }
                case BtResultType.Running:
                    return BtNodeResult.Running();
                case BtResultType.StateTransition:
                {
                    CurrentNode.OnExit();
                    return BtNodeResult.ChangeState(currentNodeResult.TargetState);
                }
                case BtResultType.NotRun:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return BtNodeResult.Success();
        }

        public void OnExit()
        {
        }
    }
}
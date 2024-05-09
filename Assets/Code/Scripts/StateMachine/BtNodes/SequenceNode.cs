using System;
using System.Collections.Generic;

namespace StateMachine.BtNodes
{
    public class SequenceNode: IBtNode
    {
        private readonly IBtNode[] _nodes;
        private IBtNode CurrentNode => _nodes[_currentNodeIndex];
        private int _currentNodeIndex = 0;

        public SequenceNode(IBtNode[] nodes)
        {
            if (nodes is null || nodes.Length == 0)
                throw new Exception("Child nodes have no items");
            
            _nodes = nodes;
        }

        public void OnEnter()
        {
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
                    break;
                }
                case BtResultType.Failure:
                    return BtNodeResult.Failure();
                case BtResultType.Running:
                    return BtNodeResult.Running();
                case BtResultType.StateTransition:
                    return BtNodeResult.ChangeState(currentNodeResult.TargetState);
                case BtResultType.NotRun:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return BtNodeResult.Success();
        }

        public void OnExit() { }
    }
}
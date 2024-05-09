using System;
using System.Collections.Generic;

namespace Code.Scripts.StateMachine
{
    public interface IStateMachine
    {
        public void Tick();
    }

    public class Transition<TBlackBoard>
    {
        private readonly Predicate<TBlackBoard> _monitor;
        public IState<TBlackBoard> TargetState { get; }
        public Transition(Predicate<TBlackBoard> monitor, IState<TBlackBoard> target)
        {
            _monitor = monitor;
            TargetState = target;
        }

        public bool Evaluate(TBlackBoard blackBoard)
        {
            return _monitor(blackBoard);
        }
    }

    public class StateMachine<TBlackBoard>: IStateMachine
    {
        private readonly IState<TBlackBoard> _initialState;
        private readonly TBlackBoard _blackBoard;
        private IState<TBlackBoard> _currentState;

        private Dictionary<IState<TBlackBoard>, List<Transition<TBlackBoard>>> _transitions = new ();

        public StateMachine(IState<TBlackBoard> initialState, TBlackBoard blackBoard)
        {
            _initialState = initialState;
            _blackBoard = blackBoard;
        }

        public void Tick()
        {
            if (_currentState is null)
                EnterState(_initialState);

            _currentState.Tick();

            ExecuteMonitor();
        }

        public void AddTransition(IState<TBlackBoard> from, IState<TBlackBoard> to, Predicate<TBlackBoard> condition)
        {
            if (_transitions.TryGetValue(from, out var existingTransitions))
            {
                existingTransitions.Add(new Transition<TBlackBoard>(condition, to));
            }
            else
            {
                _transitions.Add(from, new List<Transition<TBlackBoard>> { new (condition, to) });
            } 
        }

        public void SetState(IState<TBlackBoard> state)
        {
            EnterState(state);
        }

        private void ExecuteMonitor()
        {
            if (!_transitions.TryGetValue(_currentState, out var transitions)) return;
            foreach (var transition in transitions)
            {
                if (transition.Evaluate(_blackBoard))
                {
                    EnterState(transition.TargetState);
                    return;
                }
            }
        }

        private void EnterState(IState<TBlackBoard> targetState)
        {
            _currentState?.OnExit();
            targetState.OnEnter();
            _currentState = targetState;
        }
    }
}
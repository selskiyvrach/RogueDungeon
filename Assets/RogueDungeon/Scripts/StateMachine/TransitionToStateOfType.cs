using System;

namespace RogueDungeon.StateMachine
{
    public class TransitionToStateOfType<T> : TransitionToStateOfType where T : IState
    {
        public TransitionToStateOfType(ICondition condition) : base(typeof(T), condition)
        {
        }
    }

    public interface ITransition
    {
        bool CanTransit(StatesContainer statesContainer, out IState transitionTo);
    }

    public class TransitionToState : ITransition
    {
        private readonly IState _state;
        private readonly ICondition _condition;

        public TransitionToState(IState state, ICondition condition)
        {
            _state = state;
            _condition = condition;
        }

        public bool CanTransit(StatesContainer statesContainer, out IState transitionTo)
        {
            transitionTo = _condition.IsMet() 
                ? statesContainer.GetState(_state) ?? throw new Exception("States container does not contain this state: " + _state?.GetType().Name)
                : null;
            return transitionTo != null;
        }
    }

    public class TransitionToStateOfType : ITransition
    {
        private readonly Type _to;
        private readonly ICondition _condition;

        public TransitionToStateOfType(Type toState, ICondition condition)
        {
            _to = toState;
            _condition = condition;
        }

        public bool CanTransit(StatesContainer statesContainer, out IState transitionTo)
        {
            transitionTo = _condition.IsMet() ? statesContainer.GetState(_to) : null;
            return transitionTo != null;
        }
    }
}
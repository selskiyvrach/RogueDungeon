using System;

namespace RogueDungeon.StateMachine
{
    public class Transition<T> : Transition where T : IState
    {
        public Transition(ICondition condition) : base(typeof(T), condition)
        {
        }
    }

    public class Transition
    {
        public Type To { get; }
        public ICondition Condition { get; }

        public Transition(Type toState, ICondition condition)
        {
            To = toState;
            Condition = condition;
        }
    }
}
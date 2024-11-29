using System;

namespace Common.FSM
{
    public class FuncCondition : ICondition
    {
        private readonly Func<bool> _predicate;

        public FuncCondition(Func<bool> predicate) => 
            _predicate = predicate;

        public bool IsMet() => 
            _predicate.Invoke();
    }
}
using System;

namespace Common.FSM
{
    public class If : ICondition
    {
        private readonly Func<bool> _predicate;

        public If(Func<bool> predicate) => 
            _predicate = predicate;

        public bool IsMet() => 
            _predicate.Invoke();
    }
}
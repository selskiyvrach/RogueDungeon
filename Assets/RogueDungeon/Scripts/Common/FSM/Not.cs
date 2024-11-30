using System;

namespace Common.FSM
{
    public class Not : ICondition
    {
        private readonly ICondition _condition;

        public Not(ICondition condition) => 
            _condition = condition;

        public Not(Func<bool> condition) =>
            _condition = new If(condition);

        public bool IsMet() => 
            !_condition.IsMet();
    }
}
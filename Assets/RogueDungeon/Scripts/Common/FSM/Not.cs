namespace Common.FSM
{
    public class Not : ICondition
    {
        private readonly ICondition _condition;

        public Not(ICondition condition) => 
            _condition = condition;

        public bool IsMet() => 
            !_condition.IsMet();
    }
}
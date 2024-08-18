namespace RogueDungeon.StateMachine
{
    public class ConditionNegator : ICondition
    {
        private readonly ICondition _condition;

        public ConditionNegator(ICondition condition) => 
            _condition = condition;

        public bool IsMet() => 
            !_condition.IsMet();
    }
}
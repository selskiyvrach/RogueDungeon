namespace RogueDungeon.StateMachine
{
    public class Negator : ICondition
    {
        private readonly ICondition _condition;

        public Negator(ICondition condition) => 
            _condition = condition;

        public bool IsMet() => 
            !_condition.IsMet();
    }
}
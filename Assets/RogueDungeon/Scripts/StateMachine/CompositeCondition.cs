namespace RogueDungeon.StateMachine
{
    public class CompositeCondition : ICondition
    {
        private readonly ICondition[] _conditions;

        public CompositeCondition(params ICondition[] conditions) => 
            _conditions = conditions;
        
        public bool IsMet()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.IsMet())
                    return false;
            }

            return true;
        }
    }
}
using System.Linq;

namespace RogueDungeon.StateMachine
{
    public class IfAllCondition : CompositeCondition
    {
        public IfAllCondition(params ICondition[] conditions) : base(conditions)
        {
        }
        
        protected override bool IsMetInternal(ICondition[] conditions) => 
            conditions.All(condition => condition.IsMet());
    }
    
    public class IfAnyCondition : CompositeCondition
    {
        public IfAnyCondition(ICondition[] conditions) : base(conditions)
        {
        }

        protected override bool IsMetInternal(ICondition[] conditions) => 
            conditions.Any(condition => condition.IsMet());
    }

    public abstract class CompositeCondition : ICondition
    {
        private readonly ICondition[] _conditions;

        protected CompositeCondition(ICondition[] conditions) => 
            _conditions = conditions;

        public bool IsMet() => IsMetInternal(_conditions);
        protected abstract bool IsMetInternal(ICondition[] conditions);
    }
}
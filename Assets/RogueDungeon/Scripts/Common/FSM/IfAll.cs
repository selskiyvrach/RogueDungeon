using System.Linq;
using Common.DebugTools;
using Common.DotNetUtils;

namespace Common.FSM
{
    public class IfAll : CompositeCondition
    {
        public IfAll(params ICondition[] conditions) : base(conditions)
        {
        }
        
        protected override bool IsMetInternal(ICondition[] conditions) => 
            conditions.All(condition => condition.IsMet());
    }
    
    public class IfAny : CompositeCondition
    {
        public IfAny(params ICondition[] conditions) : base(conditions)
        {
        }

        protected override bool IsMetInternal(ICondition[] conditions) => 
            conditions.Any(condition => condition.IsMet());
    }

    public abstract class CompositeCondition : ICondition
    {
        private readonly ICondition[] _conditions;

        protected CompositeCondition(params ICondition[] conditions)
        {
            if(conditions.IsNullOrEmpty())
                Logger.LogError(this, "Null or empty conditions");
            _conditions = conditions;
        }

        public bool IsMet() => IsMetInternal(_conditions);
        protected abstract bool IsMetInternal(ICondition[] conditions);
    }
}
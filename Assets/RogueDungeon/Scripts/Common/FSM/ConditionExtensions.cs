namespace Common.FSM
{
    public static class ConditionExtensions
    {
        public static Not Negate(this ICondition condition) => 
            new(condition);
    }
}
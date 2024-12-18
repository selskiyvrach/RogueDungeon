namespace RogueDungeon.Items
{
    public interface IAttackExecutionDuration
    {
        float Value { get; }
    }

    public interface IIdleAnimationSpeed
    {
        float Value { get; }
    }

    public interface IAttackAttackTransitionDuration
    {
        float Value { get; }
    }

    public interface IAttackIdleTransitionDuration
    {
        float Value { get; }
    }
    
    public interface IUnsheathDuration
    {
        float Value { get; }
    }

    public interface IItemWeight
    {
        float Value { get; }
    }

    public interface IStrengthAttribute
    {
        int Value { get; }
    }

    public interface IAgilityAttribute
    {
        int Value { get; }
    }
    
    // test context scope decorators

    public class AttackParameters : IAttackExecutionDuration, IAttackAttackTransitionDuration, IAttackIdleTransitionDuration, IUnsheathDuration
    {
        private readonly IAttackExecutionDuration _baseAttackExecutionDuration;
        private readonly IAttackAttackTransitionDuration _baseAttackAttackTransitionDuration;
        private readonly IAttackIdleTransitionDuration _baseAttackIdleTransitionDuration;
        private readonly IUnsheathDuration _baseUnsheathDuration;
        private readonly IStrengthAttribute _strengthAttribute;
        private readonly IAgilityAttribute _agilityAttribute;

        float IAttackExecutionDuration.Value => Calculate(_baseAttackExecutionDuration.Value);
        float IAttackAttackTransitionDuration.Value => Calculate(_baseAttackAttackTransitionDuration.Value);
        float IAttackIdleTransitionDuration.Value => Calculate(_baseAttackIdleTransitionDuration.Value);
        float IUnsheathDuration.Value => Calculate(_baseUnsheathDuration.Value);
        
        // meant to be changed according to the current item
        public IItemWeight ItemWeight { get; set; }

        private float Calculate(float baseValue) => 
            baseValue * CalculateBonus();

        private float CalculateBonus()
        {
            var itemStrToAgRatio = ItemWeight.Value / 10;
            var strEffect = GetAttributeBonus(_strengthAttribute.Value) * itemStrToAgRatio;
            var agEffect = GetAttributeBonus(_agilityAttribute.Value) * (1 - itemStrToAgRatio);
            return 1 + strEffect + agEffect;
        }

        private float GetAttributeBonus(int value) => 
            ((float)value - 5) / 5;
    }
}
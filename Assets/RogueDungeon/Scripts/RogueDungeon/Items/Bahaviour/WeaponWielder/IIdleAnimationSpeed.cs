using Common.Parameters;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public interface IIdleAnimationSpeed
    {
        float Value { get; }
    }

    public class IdleAnimationSpeed : Parameter, IIdleAnimationSpeed 
    {
        public IdleAnimationSpeed(float value) : base(value)
        {
        }
    }
}
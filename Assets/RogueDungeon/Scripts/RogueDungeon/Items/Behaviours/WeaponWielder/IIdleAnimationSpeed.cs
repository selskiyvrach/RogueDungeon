using System;
using Common.Parameters;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public interface IIdleAnimationSpeed
    {
        float Value { get; }
    }

    public class IdleAnimationSpeed : Parameter, IIdleAnimationSpeed 
    {
        public IdleAnimationSpeed(Func<float> value) : base(value)
        {
        }
    }
}
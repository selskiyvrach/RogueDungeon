using Common.Parameters;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public interface IAttackAttackTransitionDuration : IParameter
    {
    }

    public class AttackAttackTransitionDuration : Parameter, IAttackAttackTransitionDuration
    {
        public AttackAttackTransitionDuration(float value) : base(value)
        {
        }
    }
}
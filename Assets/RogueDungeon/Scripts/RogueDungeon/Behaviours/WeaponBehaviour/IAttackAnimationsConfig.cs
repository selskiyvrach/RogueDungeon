using RogueDungeon.Animations;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackAnimationsConfig
    {
        AnimationConfig PrepareAnimation { get; }
        AnimationConfig ExecuteAnimation { get; }
        AnimationConfig FinishAnimation { get; }
    }
}
using RogueDungeon.Animations;

namespace RogueDungeon.Weapons
{
    public interface IAttackAnimationsProvider
    {
        AnimationConfig PrepareAnimation { get; }
        AnimationConfig ExecuteAnimation { get; }
        AnimationConfig FinishAnimation { get; }
    }
}
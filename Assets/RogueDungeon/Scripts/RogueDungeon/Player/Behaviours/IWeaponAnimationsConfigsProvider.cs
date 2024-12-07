using RogueDungeon.Animations;

namespace RogueDungeon.Player.Behaviours
{
    public interface IWeaponAnimationsConfigsProvider
    {
        AnimationConfig AttackPrepare { get; }
        AnimationConfig AttackExecute { get; }
        AnimationConfig AttackFinish { get; }
        AnimationConfig WeaponIdle { get; }
    }
}
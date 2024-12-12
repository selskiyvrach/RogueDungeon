using Common.ScreenSpaceEffects;

namespace RogueDungeon.Weapons
{
    public interface IWeaponAnimator
    {
        void PlayHit(ScreenSpaceDirection direction);
        void PlayIdle();
        void PlayPrepareAttack(float duration);
        void PlayFinishAttackLeft(float duration);
        void PlayFinishAttackRight(float duration);
    }
}
using Common.ScreenSpaceEffects;

namespace RogueDungeon.Weapons
{
    public class AttackAnimatorFacade : IWeaponAnimator
    {
        private readonly WeaponWorldSpaceAnimator _worldSpaceAnimator;
        private readonly WeaponScreenSpaceAnimator _screenSpaceAnimator;

        public AttackAnimatorFacade(WeaponWorldSpaceAnimator worldSpaceAnimator, WeaponScreenSpaceAnimator screenSpaceAnimator)
        {
            _worldSpaceAnimator = worldSpaceAnimator;
            _screenSpaceAnimator = screenSpaceAnimator;
        }

        public void PlayHit(ScreenSpaceDirection direction) => 
            _screenSpaceAnimator.PlayHit(direction);

        public void PlayIdle() => 
            _worldSpaceAnimator.PlayIdle();

        public void PlayPrepareAttack(float duration) => 
            _worldSpaceAnimator.PlayPrepareAttack(duration);

        public void PlayFinishAttackLeft(float duration) => 
            _worldSpaceAnimator.PlayFinishAttackLeft(duration);

        public void PlayFinishAttackRight(float duration) => 
            _worldSpaceAnimator.PlayFinishAttackRight(duration);
    }
}
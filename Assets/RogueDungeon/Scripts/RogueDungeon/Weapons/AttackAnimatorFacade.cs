using Common.ScreenSpaceEffects;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class AttackAnimatorFacade : MonoBehaviour, IWeaponAnimator
    {
        [SerializeField] private WeaponWorldSpaceAnimator _worldSpaceAnimator;
        [SerializeField] private WeaponScreenSpaceAnimator _screenSpaceAnimator;
        
        public void PlayHit(ScreenSpaceDirection direction) => 
            _screenSpaceAnimator.PlayHit(direction);

        public void ResetCurrentAnimation() => 
            _worldSpaceAnimator.ResetCurrentAnimation();

        public void PlayPrepareAttack(float duration) => 
            _worldSpaceAnimator.PlayPrepareAttack(duration);

        public void PlayFinishAttackLeft(float duration) => 
            _worldSpaceAnimator.PlayFinishAttackLeft(duration);

        public void PlayFinishAttackRight(float duration) => 
            _worldSpaceAnimator.PlayFinishAttackRight(duration);
    }
}
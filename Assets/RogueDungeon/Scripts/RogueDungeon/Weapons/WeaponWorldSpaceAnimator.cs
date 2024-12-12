using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class WeaponWorldSpaceAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private AnimationClip _prepareAttackAnimation;
        [SerializeField] private AnimationClip _idleAnimation;
        [SerializeField] private AnimationClip _finishAttackRightAnimation;
        [SerializeField] private AnimationClip _finishAttackLeftAnimation;
        
        private static readonly int PrepareAttackHash = Animator.StringToHash("prepare");
        private static readonly int FinishRightAttackHash = Animator.StringToHash("finish_right");
        private static readonly int FinishLeftAttackHash = Animator.StringToHash("finish_left");
        private static readonly int SpeedHash = Animator.StringToHash("speed");
        private static readonly int IdleHash = Animator.StringToHash("idle");
        
        public void ResetCurrentAnimation() => 
            PlayHandAnimation(IdleHash, _idleAnimation.length, _idleAnimation.length);

        public void PlayPrepareAttack(float duration) => 
            PlayHandAnimation(PrepareAttackHash, _prepareAttackAnimation.length, duration);

        public void PlayFinishAttackLeft(float duration) => 
            PlayHandAnimation(FinishLeftAttackHash, _finishAttackLeftAnimation.length, duration);
        
        public void PlayFinishAttackRight(float duration) => 
            PlayHandAnimation(FinishRightAttackHash, _finishAttackRightAnimation.length, duration);

        private void PlayHandAnimation(int hash, float animationLength, float duration)
        {
            _animator.SetTrigger(hash);
            _animator.SetFloat(SpeedHash, animationLength / duration);
        }
    }
}
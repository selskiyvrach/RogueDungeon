using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class WeaponWorldSpaceAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private AnimationClip _unsheathAnimation;
        [SerializeField] private AnimationClip _sheathAnimation;
        [SerializeField] private AnimationClip _prepareAttackAnimation;
        [SerializeField] private AnimationClip _finishAttackRightAnimation;
        [SerializeField] private AnimationClip _finishAttackLeftAnimation;
        
        private static readonly int UnsheathHash = Animator.StringToHash("unsheath");
        private static readonly int SheathHash = Animator.StringToHash("sheath");
        private static readonly int PrepareAttackHash = Animator.StringToHash("prepare_attack");
        private static readonly int FinishAttackHash = Animator.StringToHash("finish_attack");
        private static readonly int SpeedHash = Animator.StringToHash("speed");
        private static readonly int IdleHash = Animator.StringToHash("idle");
        
        public void ResetCurrentAnimation() => 
            _animator.SetTrigger(IdleHash);

        public void PlayUnsheath(float duration) => 
            PlayHandAnimation(UnsheathHash, _unsheathAnimation.length, duration);

        public void PlaySheath(float duration) => 
            PlayHandAnimation(SheathHash, _sheathAnimation.length, duration);

        public void PlayPrepareAttack(float duration) => 
            PlayHandAnimation(PrepareAttackHash, _prepareAttackAnimation.length, duration);

        public void PlayFinishAttackLeft(float duration) => 
            PlayHandAnimation(FinishAttackHash, _finishAttackLeftAnimation.length, duration);
        
        public void PlayFinishAttackRight(float duration) => 
            PlayHandAnimation(FinishAttackHash, _finishAttackRightAnimation.length, duration);

        private void PlayHandAnimation(int hash, float animationLength, float duration)
        {
            _animator.SetTrigger(hash);
            _animator.SetFloat(SpeedHash, animationLength / duration);
        }
    }
}
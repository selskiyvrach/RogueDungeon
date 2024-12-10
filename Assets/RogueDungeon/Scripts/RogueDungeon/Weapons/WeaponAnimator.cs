using Common.GameObjectMarkers;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class WeaponAnimator : AttackStateChangedHandler
    {
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private Animator _screenEffectsAnimator;
        
        private readonly IAttackMediator _attackMediator;
        private readonly WeaponAnimationRootObject _animationRoot;

        public WeaponAnimator(IAttackMediator attackMediator, WeaponAnimationRootObject animationRoot) : base(attackMediator)
        {
            _attackMediator = attackMediator;
            _animationRoot = animationRoot;
            _attackMediator.OnHitKeyframe.Subscribe(_ => PlayHitAnimation());
        }

        private void PlayHitAnimation()
        {
            // play hit based on direction
        }

        protected override void HandleStateChanged(AttackState state)
        {
            // play idle
            // play prepare
            // play finished
        }
    }
}
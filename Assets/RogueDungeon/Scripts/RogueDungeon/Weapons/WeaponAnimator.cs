using System;
using Common.GameObjectMarkers;
using RogueDungeon.Animations;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class WeaponAnimator : AttackStateChangedHandler
    {
        private readonly IAttackMediator _attackMediator;
        private readonly WeaponAnimationRootObject _animationRoot;
        private readonly IWeaponAnimationsConfig _animationsConfig;
        private readonly IAttackComboConfig _comboConfig;

        private readonly AnimationPlayer _animationPlayer = new();

        public WeaponAnimator(
            IAttackMediator attackMediator, 
            IWeaponAnimationsConfig animationsConfig, 
            WeaponAnimationRootObject animationRoot, 
            IAttackComboConfig comboConfig) : base(attackMediator)
        {
            _attackMediator = attackMediator;
            _animationsConfig = animationsConfig;
            _animationRoot = animationRoot;
            _comboConfig = comboConfig;
            _animationPlayer.OnEvent.Subscribe(HandleEvent);
        }

        private void HandleEvent(string value)
        {
            if(value == AnimEventNames.ATTACK_HIT)
                _attackMediator.OnHitKeyframe();
            else
                Debug.LogError($"Unexpected animation event in {nameof(WeaponAnimator)}: " + value);
        }

        protected override void HandleStateChanged(AttackState state)
        {
            if (state == AttackState.None)
            {
                _animationPlayer.Play(_animationsConfig.IdleAnimation, _animationRoot);
                return;
            }

            var animConfig = _animationsConfig.GetAttackAnimationConfig(_attackMediator.ComboIndex);
            var timingConfig = _comboConfig.GetTimings(_attackMediator.ComboIndex);
            _animationPlayer.Play(state switch
                {
                    AttackState.Preparing => animConfig.PrepareAnimation,
                    AttackState.Executing => animConfig.ExecuteAnimation,
                    AttackState.Finishing => animConfig.FinishAnimation,
                    _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
                }, 
                _animationRoot, 
                state switch 
                {
                    AttackState.Preparing => timingConfig.GetPrepareDuration(),
                    AttackState.Executing => timingConfig.GetExecuteDuration(),
                    AttackState.Finishing => timingConfig.GetFinishDuration(),
                    _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
                }
            );
        }
    }
}
using System;
using Common.GameObjectMarkers;
using RogueDungeon.Animations;
using RogueDungeon.Behaviours.MovementBehaviour;
using RogueDungeon.Behaviours.WeaponBehaviour;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours
{
    public interface IPlayerMovementAnimationConfigsProvider
    {
        AnimationConfig DodgeLeft { get; }
        AnimationConfig DodgeRight { get; }
        AnimationConfig Idle { get; }
    }

    public class PlayerMovementAnimationConfigsProvider : ScriptableObject, IPlayerMovementAnimationConfigsProvider
    {
        [field: SerializeField] public AnimationConfig DodgeLeft { get; private set;}
        [field: SerializeField] public AnimationConfig DodgeRight { get; private set;}
        [field: SerializeField] public AnimationConfig Idle { get; private set;}
    }

    public class WeaponAnimationsPlayer : IWeaponAnimationsPlayer
    {
        private readonly IWeaponAnimationsConfigsProvider _configsProvider;
        private readonly WeaponAnimationRootObject _weaponAnimationRootObject;
        private readonly AnimationPlayer _animationPlayer;

        public event Action OnHitKeyframe;

        public WeaponAnimationsPlayer()
        {
            _animationPlayer = new AnimationPlayer();
            _animationPlayer.OnEvent.Subscribe(name =>
            {
                if (name == AnimEventNames.ATTACK_HIT)
                    OnHitKeyframe?.Invoke();
                else
                    Debug.LogError($"Unexpected event name in {nameof(WeaponAnimationsPlayer)}: " + name);
            });
        }

        public void PlayAttackPrepare(float duration) => 
            _animationPlayer.Play(_configsProvider.AttackPrepare, _weaponAnimationRootObject, duration);

        public void PlayAttackExecute(float duration) => 
            _animationPlayer.Play(_configsProvider.AttackExecute, _weaponAnimationRootObject, duration);

        public void PlayAttackFinish(float duration) => 
            _animationPlayer.Play(_configsProvider.AttackFinish, _weaponAnimationRootObject, duration);
        
        public void PlayIdle() => 
            _animationPlayer.Play(_configsProvider.WeaponIdle, _weaponAnimationRootObject);
    }

    public class PlayerMovementAnimationsPlayer : IMovementAnimationsPlayer
    {
        private readonly IPlayerMovementAnimationConfigsProvider _animationConfigsProvider;
        private readonly CharacterMovementAnimationRootObject _characterAnimationRoot;

        private readonly AnimationPlayer _animationPlayer = new();
        
        public void PlayDodgeLeft(float duration) => 
            _animationPlayer.Play(_animationConfigsProvider.DodgeLeft, _characterAnimationRoot, duration);

        public void PlayDodgeRight(float duration) => 
            _animationPlayer.Play(_animationConfigsProvider.DodgeRight, _characterAnimationRoot, duration);
        
        public void PlayIdle() => 
            _animationPlayer.Play(_animationConfigsProvider.Idle, _characterAnimationRoot);
    }
}
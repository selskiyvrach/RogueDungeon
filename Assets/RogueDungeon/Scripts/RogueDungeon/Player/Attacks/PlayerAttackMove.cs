using Common.Animations;
using RogueDungeon.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class PlayerAttackMove : PlayerAttackBaseMove
    {
        private readonly IPlayerAttacksMediator _playerAttacksMediator;
        private readonly IWeapon _weapon;
        private readonly PlayerControlStateMediator _playerControlStateMediator;
        private readonly PlayerAttackMoveConfig _config;
        private readonly IPlayerInput _input;
        
        public PlayerAttackMove(IPlayerInput input, PlayerAttackMoveConfig config, IAnimation animation, PlayerControlStateMediator playerControlStateMediator,
            IPlayerAttacksMediator playerAttacksMediator, PlayerControlStateMediator controlStateMediator, IWeapon weapon) : base(config, animation, input, controlStateMediator)
        {
            _config = config;
            _playerControlStateMediator = playerControlStateMediator;
            _playerAttacksMediator = playerAttacksMediator;
            _weapon = weapon;
            _input = input;
        }

        public override void Enter()
        {
            base.Enter();
            if(_config.RequiredInputKey != InputKey.None)
                _input.ConsumeInput(_config.RequiredInputKey);
            _playerControlStateMediator.IsAttackInUncancellableState = _config.IsUncancellable;
        }

        public override void Exit()
        {
            base.Exit();
            _playerControlStateMediator.IsAttackInUncancellableState = false;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            base.OnAnimationEvent(name);
            if (name == AnimationEventNames.Hit)
                _playerAttacksMediator.MediatePlayerAttack(_weapon);
            else
                Debug.LogError("Attack move lacks implementation for handling animation event: " + name);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_config.RequiredInputKey == InputKey.None || _input.HasInput(_config.RequiredInputKey));
    }
}
using Common.Animations;
using RogueDungeon.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;
using UnityEngine;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerAttackMove : PlayerMove
    {
        private readonly IPlayerAttacksMediator _playerAttacksMediator;
        private readonly IWeapon _weapon;
        private readonly PlayerControlStateMediator _playerControlStateMediator;
        
        public PlayerAttackMove(IPlayerInput input, PlayerAttackMoveConfig config, IAnimation animation, PlayerControlStateMediator playerControlStateMediator,
            IPlayerAttacksMediator playerAttacksMediator, IWeapon weapon) : base(config, animation, input)
        {
            _playerControlStateMediator = playerControlStateMediator;
            _playerAttacksMediator = playerAttacksMediator;
            _weapon = weapon;
        }

        public override void Enter()
        {
            base.Enter();
            _playerControlStateMediator.IsAttackInUncancellableState = true;
        }

        public override void Exit()
        {
            base.Exit();
            _playerControlStateMediator.IsAttackInUncancellableState = false;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            if (name == AnimationEventNames.Hit)
                _playerAttacksMediator.MediatePlayerAttack(_weapon);
            else
                Debug.LogError("Attack move lacks implementation for handling animation event: " + name);
        }
    }
}
using Common.Animations;
using RogueDungeon.Animations;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;
using UnityEngine;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemExecuteAttackMove : PlayerMove
    {
        private readonly IPlayerAttacksMediator _playerAttacksMediator;
        private readonly IWeapon _weapon;
        private readonly PlayerControlStateMediator _playerControlStateMediator;

        protected override float Duration => ((WeaponConfig)_weapon.Config).AttackExecuteDuration;

        public ItemExecuteAttackMove(IAnimation animation,
            PlayerControlStateMediator playerControlStateMediator,
            IPlayerAttacksMediator playerAttacksMediator, IWeapon weapon, string id) : base(id, animation)
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
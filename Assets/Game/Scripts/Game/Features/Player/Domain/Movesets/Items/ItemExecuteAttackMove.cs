using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.Animations;
using Game.Libs.Input;
using Game.Libs.Items;
using Libs.Animations;
using UnityEngine;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class ItemExecuteAttackMove : ItemMove
    {
        private readonly IWeapon _weapon;
        private readonly IAttackItemWielder _wielder;

        protected override float Duration => _weapon.AttackExecuteAnimationDuration;

        public ItemExecuteAttackMove(IAnimation animation, IAttackItemWielder wielder, IWeapon weapon, string id, IPlayerInput input) : base(id, animation, wielder, input)
        {
            _wielder = wielder;
            _weapon = weapon;
        }

        public override void Enter()
        {
            base.Enter();
            _wielder.IsAttackInUncancellableState = true;
        }

        public override void Exit()
        {
            base.Exit();
            _wielder.IsAttackInUncancellableState = false;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            if (name == AnimationEventNames.HIT)
                _wielder.PerformAttack(_weapon);
            else
                Debug.LogError("Attack move lacks implementation for handling animation event: " + name);
        }
    }
}
using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;
using UnityEngine;

namespace Game.Features.Items.Domain.Moves
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
            if (name == AnimationEventNames.Hit)
                _wielder.PerformAttack(_weapon);
            else
                Debug.LogError("Attack move lacks implementation for handling animation event: " + name);
        }
    }
}
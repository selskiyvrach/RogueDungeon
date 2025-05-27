using Common.Animations;
using Input;
using RogueDungeon.Items.Model.Configs;
using UnityEngine;

namespace RogueDungeon.Items.Model.Moves
{
    public class ItemExecuteAttackMove : ItemMove
    {
        private readonly IWeapon _weapon;
        private readonly IAttackItemWielder _wielder;

        protected override float Duration => ((WeaponConfig)_weapon.Config).AttackExecuteDuration;

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
using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Combat;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    // idle + attack input -> prepare -> execute -> finish -> idle
    // execute + attack input -> next attack transition -> execute
    public abstract class AttackMove : Move
    {
        private readonly IAttacksMediator _attacksMediator;
        private readonly AttackMoveConfig _config;

        protected AttackMove(AttackMoveConfig config, IAnimator animator, IAttacksMediator attacksMediator) : base(config, animator)
        {
            _config = config;
            _attacksMediator = attacksMediator;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            if (name == "hit")
            {
                if(_config.JustAnimation)
                    Debug.LogError("Unsupposed Animation Event: " + name);
                else
                    HandleAttack(_attacksMediator);
            }
            else
                Debug.LogError("Attack move lacks implementation for handling animation event: " + name);
        }

        protected abstract void HandleAttack(IAttacksMediator attacksMediator);
    }
}
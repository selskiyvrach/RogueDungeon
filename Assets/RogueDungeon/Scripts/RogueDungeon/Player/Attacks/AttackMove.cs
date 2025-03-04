using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Player;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public abstract class AttackMove : Move
    {
        private readonly IPlayerAttacksMediator _playerAttacksMediator;
        private readonly AttackMoveConfig _config;

        protected AttackMove(AttackMoveConfig config, IAnimator animator, IPlayerAttacksMediator playerAttacksMediator) : base(config, animator)
        {
            _config = config;
            _playerAttacksMediator = playerAttacksMediator;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            if (name == "hit")
            {
                if(_config.JustAnimation)
                    Debug.LogError("Unsupposed Animation Event: " + name);
                else
                    HandleAttack(_playerAttacksMediator);
            }
            else
                Debug.LogError("Attack move lacks implementation for handling animation event: " + name);
        }

        protected abstract void HandleAttack(IPlayerAttacksMediator playerAttacksMediator);
    }
}
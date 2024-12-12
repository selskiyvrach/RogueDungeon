using System;
using Common.ScreenSpaceEffects;

namespace RogueDungeon.Weapons
{
    public class AttackAnimatorController : AttackBehaviourHandler
    {
        private readonly IWeaponAnimator _animator;

        public AttackAnimatorController(IWeaponAnimator animator, IAttackBehaviour attackBehaviour) : base(attackBehaviour) => 
            _animator = animator;

        protected override void HandleIdle() => 
            _animator.PlayIdle();

        protected override void HandleExecute()
        {
        }

        protected override void HandleFinish()
        {
            switch (Behaviour.CurrentAttackDirection)
            {
                case ScreenSpaceDirection.BottomLeft or ScreenSpaceDirection.TopLeft or ScreenSpaceDirection.Left:
                    _animator.PlayFinishAttackLeft(Behaviour.Durations.IdleAttackTransition);
                    break;
                case ScreenSpaceDirection.BottomRight or ScreenSpaceDirection.TopRight or ScreenSpaceDirection.Right:
                    _animator.PlayFinishAttackRight(Behaviour.Durations.IdleAttackTransition);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void HandlePrepare() => 
            _animator.PlayPrepareAttack(Behaviour.Durations.IdleAttackTransition);

        protected override void HandleHit() => 
            _animator.PlayHit(Behaviour.CurrentAttackDirection);
    }
}
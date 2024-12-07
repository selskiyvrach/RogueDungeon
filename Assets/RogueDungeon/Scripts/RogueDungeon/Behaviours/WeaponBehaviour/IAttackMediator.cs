using System;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackMediator
    {
        bool CanStartAttack();
        int ComboIndex { get; set; }
        AttackState AttackState { get; set; }
        event Action<AttackState> OnAttackStateChanged;
    }

    public abstract class AttackStateChangedHandler
    {
        private readonly IAttackMediator _attackMediator;

        protected AttackStateChangedHandler(IAttackMediator attackMediator)
        {
            _attackMediator = attackMediator;
            _attackMediator.OnAttackStateChanged += HandleState;
        }

        protected abstract void HandleState(AttackState state);
    }
    
    public class PlayAnimationsAttackStateHandler : AttackStateChangedHandler
    {
        public PlayAnimationsAttackStateHandler(IAttackMediator attackMediator) : base(attackMediator) { }

        protected override void HandleState(AttackState state)
        {
            // weapon config
            // attack index
        }
    }
}
using System;
using RogueDungeon.Behaviours.MovementBehaviour;
using RogueDungeon.Behaviours.WeaponBehaviour;

namespace RogueDungeon.Player.Behaviours
{
    public class PlayerBehavioursMediator : IDodgeMediator, IAttackMediator
    {
        private AttackState _attackState;
        public DodgeState DodgeState { get; set; }

        public int ComboIndex { get; set; } = -1;

        public AttackState AttackState
        {
            get => _attackState;
            set => OnAttackStateChanged.Invoke(_attackState = value);
        }

        public event Action<AttackState> OnAttackStateChanged = delegate { };


        public bool CanStartDodge() => 
            AttackState != AttackState.Executing;

        public bool CanStartAttack() => 
            DodgeState == DodgeState.None;
    }
}
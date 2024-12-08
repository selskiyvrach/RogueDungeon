using System;
using RogueDungeon.Behaviours.MovementBehaviour;
using RogueDungeon.Weapons;
using UniRx;

namespace RogueDungeon.Player.Behaviours
{
    public class PlayerBehavioursMediator : IDodgeMediator, IAttackMediator
    {
        private AttackState _attackState;
        public DodgeState DodgeState { get; set; }

        public int ComboIndex { get; set; } = -1;

        public ReactiveProperty<AttackState> AttackState { get; } = new();

        public void OnHitKeyframe()
        {
            throw new NotImplementedException();
        }


        public bool CanStartDodge() => 
            AttackState.Value != Weapons.AttackState.Executing;

        public bool CanStartAttack() => 
            DodgeState == DodgeState.None;
    }
}
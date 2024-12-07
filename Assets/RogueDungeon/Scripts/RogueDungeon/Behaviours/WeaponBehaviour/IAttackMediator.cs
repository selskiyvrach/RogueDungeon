using System;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackMediator
    {
        bool CanStartAttack();
        int ComboIndex { get; set; }
        AttackState AttackState { get; set; }
        event Action<AttackState> OnAttackStateChanged;
        void OnHitKeyframe();
    }
}
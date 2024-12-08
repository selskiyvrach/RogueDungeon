using UniRx;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public interface IAttackMediator
    {
        bool CanStartAttack();
        int ComboIndex { get; set; }
        ReactiveProperty<AttackState> AttackState { get; }
        void OnHitKeyframe();
    }

    public class DummyAttackMediator : IAttackMediator
    {
        public bool CanStartAttack() => 
            true;

        public int ComboIndex { get; set; }
        public ReactiveProperty<AttackState> AttackState { get; } = new();

        public void OnHitKeyframe() => 
            Debug.Log("Hit keyframe pseudo-handled");
    }
}
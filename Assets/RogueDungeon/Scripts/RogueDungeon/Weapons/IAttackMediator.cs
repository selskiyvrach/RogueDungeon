using UniRx;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public interface IAttackMediator
    {
        bool CanStartAttack();
        int AttackIndex { get; set; }
        ReactiveProperty<AttackState> AttackState { get; }
        ISubject<Unit> OnHitKeyframe { get; }
    }

    public class DummyAttackMediator : IAttackMediator
    {
        public bool CanStartAttack() => 
            true;
        public int AttackIndex { get; set; }
        public ReactiveProperty<AttackState> AttackState { get; } = new();
        ISubject<Unit> IAttackMediator.OnHitKeyframe { get; } = new Subject<Unit>();
    }
}
using System;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
{
    // public class AttackInfo
    // {
    //     
    // }
    //
    // public interface IAttackAnimation
    // {
    //     event Action OnHitKeyframe;
    // }
    //
    // public class DeadState : IState, IEnterable
    // {
    //     public void Enter()
    //     {
    //         
    //     }
    // }
    //
    // public interface IAttackComboInfoProvider
    // {
    //     bool TryGetNext(out AttackInfo attackInfo);
    //     void Reset();
    // }
    //
    // public class AttackState : IState, IFinishable, IEnterable
    // {
    //     private readonly IAttackComboInfoProvider _comboInfoProvider;
    //     private int _attacksInSuccessionCount;
    //     
    //     public bool IsFinished { get; private set; }
    //
    //     public void Enter()
    //     {
    //         _comboInfoProvider.Reset();
    //         _attacksInSuccessionCount = 0;
    //         StartNextAttack();
    //     }
    //
    //     private void StartNextAttack()
    //     {
    //         if (!_comboInfoProvider.TryGetNext(out var info))
    //         {
    //             IsFinished = true;
    //             return;
    //         }
    //         
    //     }
    // }
    //
    //
    // public class DodgeLeftState : IState, IFinishable
    // {
    //     public bool IsFinished { get; }
    // }
    //
    // public class DodgeRightState : IState, IFinishable
    // {
    //     public bool IsFinished { get; }
    // }
    //
    // public class BlockState : IState, IFinishable
    // {
    //     public bool IsFinished { get; }
    // }
}
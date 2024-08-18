using System;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Player
{
    public interface IAttackAnimation : IFinishableAnimation
    {
        event Action OnHitKeyframe;
    }
}
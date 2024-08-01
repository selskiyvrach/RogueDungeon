using System;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Player
{
    public interface IAttackAnimation : IAnimation, IFinishable
    {
        event Action OnHitKeyframe;
    }
}
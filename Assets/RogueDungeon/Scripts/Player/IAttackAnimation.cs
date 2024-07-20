using System;

namespace RogueDungeon.Player
{
    public interface IAttackAnimation : IAnimation
    {
        event Action OnHitKeyframe;
    }
}
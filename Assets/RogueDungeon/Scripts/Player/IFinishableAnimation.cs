using System;

namespace RogueDungeon.Player
{
    public interface IFinishableAnimation
    {
        event Action OnCompleted;
    }
}
using System;

namespace RogueDungeon.Player
{
    public interface IWalkAnimation : IAnimation
    {
        event Action OnStepped;
    }
}
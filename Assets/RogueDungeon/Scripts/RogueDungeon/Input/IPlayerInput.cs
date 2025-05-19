using System;
using Common.Lifecycle;
using UnityEngine;

namespace RogueDungeon.Input
{
    public interface IPlayerInput : ITickable
    {
        Vector2 CursorPos { get; }
        event Action OnCursorMoved;
        InputUnit GetKey(InputKey key);
    }
}
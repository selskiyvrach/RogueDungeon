using System;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Libs.Input
{
    public interface IPlayerInput : ITickable
    {
        Vector2 CursorPos { get; }
        event Action OnCursorMoved;
        InputUnit GetKey(InputKey key);
    }
}
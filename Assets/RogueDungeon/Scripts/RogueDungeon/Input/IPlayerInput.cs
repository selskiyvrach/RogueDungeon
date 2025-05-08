using Common.Lifecycle;
using UnityEngine;

namespace RogueDungeon.Input
{
    public interface IPlayerInput : ITickable
    {
        Vector2 CursorPos { get; }
        void SetFilter(InputFilter filter);
        bool IsDown(InputKey inputKey);
        bool IsHeld(InputKey inputKey);
        void ConsumeInput(InputKey inputKey);
    }
}
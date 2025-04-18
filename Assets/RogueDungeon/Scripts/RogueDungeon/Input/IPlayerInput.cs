using Common.Lifecycle;

namespace RogueDungeon.Input
{
    public interface IPlayerInput : ITickable
    {
        void SetFilter(InputFilter filter);
        bool IsDown(InputKey inputKey);
        bool IsHeld(InputKey inputKey);
        void ConsumeInput(InputKey inputKey);
    }
}
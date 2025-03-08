using Common.Lifecycle;

namespace RogueDungeon.Input
{
    public interface IPlayerInput : ITickable
    {
        void SetFilter(InputFilter filter);
        bool HasInput(InputKey inputKey);
        void ConsumeInput(InputKey inputKey);
    }
}
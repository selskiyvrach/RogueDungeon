using Common.Behaviours;

namespace RogueDungeon.Input
{
    public interface IPlayerInput : IBehaviour
    {
        void SetFilter(InputFilter filter);
        bool HasInput(InputKey inputKey);
        void ConsumeInput(InputKey inputKey);
    }
}
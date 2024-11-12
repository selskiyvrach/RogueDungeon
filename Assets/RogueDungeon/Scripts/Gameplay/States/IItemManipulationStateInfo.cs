using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay.States
{
    public interface IItemManipulationStateInfo
    {
        ICondition EnterCondition { get; }
        IFinishableState State { get; }
    }
}
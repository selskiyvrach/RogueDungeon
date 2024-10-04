using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
{
    public interface IItemManipulator
    {
        ICondition EnterCondition { get; }
        IFinishableState State { get; }
    }
}
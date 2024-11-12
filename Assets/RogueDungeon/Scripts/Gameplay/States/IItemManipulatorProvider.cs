using UniRx;

namespace RogueDungeon.Gameplay.States
{
    public interface IItemManipulatorProvider
    {
        IReadOnlyReactiveProperty<IItemManipulationStateInfo> CurrentItemManipulator { get; }
    }
}
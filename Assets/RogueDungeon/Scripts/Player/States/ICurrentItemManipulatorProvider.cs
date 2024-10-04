using UniRx;

namespace RogueDungeon.Player.States
{
    public interface ICurrentItemManipulatorProvider
    {
        IReadOnlyReactiveProperty<IItemManipulator> CurrentManipulator { get; }
    }
}
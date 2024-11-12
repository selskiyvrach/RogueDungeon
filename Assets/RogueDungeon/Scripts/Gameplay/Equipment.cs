using RogueDungeon.Gameplay.States;
using UniRx;

namespace RogueDungeon.Gameplay
{
    public class Equipment : IItemManipulatorProvider
    {
        public IReadOnlyReactiveProperty<IItemManipulationStateInfo> CurrentItemManipulator { get; }
    }
}
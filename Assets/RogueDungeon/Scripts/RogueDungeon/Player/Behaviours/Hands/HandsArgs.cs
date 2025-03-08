using Common.Fsm;
using Common.MoveSets;
using RogueDungeon.Items;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public readonly struct HandsArgs
    {
        public IFactory<ItemConfig, HandHeldItemPresenter> PresenterFactory { get; }
        public IFactory<MoveSetConfig, StateMachine> ItemMoveSetFactory { get; }
        public StateMachine UnsheathMoveSet { get; }
        public HandHeldContext HandHeldContext { get; }

        public HandsArgs(IFactory<ItemConfig, HandHeldItemPresenter> presenterFactory, IFactory<MoveSetConfig, StateMachine> itemMoveSetFactory, StateMachine unsheathMoveSet, HandHeldContext handHeldContext)
        {
            PresenterFactory = presenterFactory;
            ItemMoveSetFactory = itemMoveSetFactory;
            UnsheathMoveSet = unsheathMoveSet;
            HandHeldContext = handHeldContext;
        }
    }
}
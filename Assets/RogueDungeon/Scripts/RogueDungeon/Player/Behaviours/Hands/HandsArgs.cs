using Common.MoveSets;
using RogueDungeon.Items;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public readonly struct HandsArgs
    {
        public IFactory<ItemConfig, HandHeldItemPresenter> PresenterFactory { get; }
        public IFactory<MoveSetConfig, MoveSetBehaviour> ItemMoveSetFactory { get; }
        public MoveSetBehaviour UnsheathMoveSet { get; }
        public HandHeldContext HandHeldContext { get; }

        public HandsArgs(IFactory<ItemConfig, HandHeldItemPresenter> presenterFactory, IFactory<MoveSetConfig, MoveSetBehaviour> itemMoveSetFactory, MoveSetBehaviour unsheathMoveSet, HandHeldContext handHeldContext)
        {
            PresenterFactory = presenterFactory;
            ItemMoveSetFactory = itemMoveSetFactory;
            UnsheathMoveSet = unsheathMoveSet;
            HandHeldContext = handHeldContext;
        }
    }
}
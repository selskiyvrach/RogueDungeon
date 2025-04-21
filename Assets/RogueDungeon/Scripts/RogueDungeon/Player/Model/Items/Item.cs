using Common.Fsm;

namespace RogueDungeon.Items
{
    public interface IItem
    {
        ItemConfig Config { get; }
        float UnsheathDuration { get; }
        HandHeldItemPresenter Presenter { get; set; }
        StateMachine Moveset { get; set; }
    }

    public interface IBlockingItem : IItem
    {
        public BlockingTier BlockingTier { get; }
        public float BlockStaminaCostMultiplier { get; }
    }

    public enum BlockingTier
    {
        None,
        Second, 
        First, 
    }
    
    // if two items equipped -> pick the one with the highest tier or the left one if both are the same

    public class Item : IItem
    {
        public ItemConfig Config { get; }
        public float UnsheathDuration => Config.UnsheathDuration;
        public HandHeldItemPresenter Presenter { get; set; }
        public StateMachine Moveset { get; set; }

        protected Item(ItemConfig config) => 
            Config = config;
    }
}
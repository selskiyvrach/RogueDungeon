namespace RogueDungeon.Items
{
    public interface IItem
    {
        ItemConfig Config { get; }
        float UnsheathDuration { get; }
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

        protected Item(ItemConfig config) => 
            Config = config;
    }
}
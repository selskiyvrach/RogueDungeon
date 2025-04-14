namespace RogueDungeon.Items
{
    public interface IItem
    {
        ItemConfig Config { get; }
        float BlockStaminaCostMultiplier { get; }
        float UnsheathDuration { get; }
    }

    public class Item : IItem
    {
        public ItemConfig Config { get; }
        public float BlockStaminaCostMultiplier => Config.BlockStaminaCostMultiplier;
        public float UnsheathDuration => Config.UnsheathDuration;

        protected Item(ItemConfig config) => 
            Config = config;
    }
}
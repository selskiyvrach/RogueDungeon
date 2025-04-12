namespace RogueDungeon.Items
{
    public interface IItem
    {
        ItemConfig Config { get; }
        float BlockStaminaCostMultiplier { get; }
    }

    public class Item : IItem
    {
        public ItemConfig Config { get; }
        public float BlockStaminaCostMultiplier => Config.BlockStaminaCostMultiplier;

        protected Item(ItemConfig config) => 
            Config = config;
    }
}
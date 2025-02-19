namespace RogueDungeon.Items
{
    public class Item
    {
        public readonly ItemConfig Config;

        public Item(ItemConfig config) => 
            Config = config;
    }
}
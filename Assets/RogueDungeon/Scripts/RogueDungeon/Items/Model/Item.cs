using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model
{
    public abstract class Item : IItem
    {
        private static int _idCounter;
        public int InstanceId { get; }
        public ItemConfig Config { get; }

        protected Item(ItemConfig config)
        {
            Config = config;
            InstanceId = _idCounter++;
        }
    }
}
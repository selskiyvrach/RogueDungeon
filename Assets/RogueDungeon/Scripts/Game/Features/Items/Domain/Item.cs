using Game.Features.Items.Domain.Configs;

namespace Game.Features.Items.Domain
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
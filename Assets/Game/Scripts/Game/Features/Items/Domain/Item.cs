namespace Game.Features.Items.Domain
{
    public abstract class Item : IItem
    {
        private readonly IItemConfig _itemConfig;
        private static int _idCounter;
        
        public string TypeId => _itemConfig.ItemTypeId;
        public int InstanceId { get; }

        protected Item(IItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
            InstanceId = _idCounter++;
        }
    }
}
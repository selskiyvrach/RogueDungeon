using JetBrains.Annotations;

namespace RogueDungeon.Items
{
    public class Item
    {
        public readonly ItemConfig Config;
        [CanBeNull] private HandHeldItemPresenter _presenter;

        public Item(ItemConfig config) => 
            Config = config;
    }
}
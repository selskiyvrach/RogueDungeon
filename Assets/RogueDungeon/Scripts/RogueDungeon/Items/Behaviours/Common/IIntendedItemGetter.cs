using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Items.Behaviours.Common
{
    public interface IIntendedItemGetter
    {
        public IItemInfo Item { get; }
    }
}
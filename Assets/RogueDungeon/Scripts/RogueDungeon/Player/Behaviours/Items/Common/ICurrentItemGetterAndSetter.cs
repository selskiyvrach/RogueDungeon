using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Items.Behaviours.Common
{
    public interface IIntendedItemSetter
    {
        IItemInfo Item { set; }
    }
}
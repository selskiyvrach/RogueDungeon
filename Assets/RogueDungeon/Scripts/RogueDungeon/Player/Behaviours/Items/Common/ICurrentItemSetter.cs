using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Items.Behaviours.Common
{
    public interface ICurrentItemSetter
    {
        IItemInfo Item { set; }
    }
}
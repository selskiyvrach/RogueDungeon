using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Items.Behaviours.Common
{
    public interface ICurrentItemGetter
    {
        IItemInfo Item { get; }
    }
}
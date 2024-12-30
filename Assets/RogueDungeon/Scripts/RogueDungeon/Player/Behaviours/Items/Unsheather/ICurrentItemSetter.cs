using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public interface ICurrentItemSetter
    {
        IItemInfo Item { set; }
    }
}
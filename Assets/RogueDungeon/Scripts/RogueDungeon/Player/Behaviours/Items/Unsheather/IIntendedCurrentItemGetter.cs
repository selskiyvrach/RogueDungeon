using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public interface IIntendedCurrentItemGetter
    {
        public IItemInfo Item { get; }
    }
}
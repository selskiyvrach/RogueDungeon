using Common.Behaviours;
using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class UnsheatherInternalFacade : IBehaviourInternalFacade, 
        ICurrentItemSetter, 
        ICurrentItemGetter, 
        IIntendedCurrentItemGetter,
        IIntendedCurrentItemSetter,
        ICurrentItemUsableSetter, 
        ICurrentItemUsableGetter
    {
        // CURRENT ITEM
        private IItemInfo _currentItem;
        IItemInfo ICurrentItemGetter.Item => _currentItem;
        public IItemInfo Item
        {
            set => _currentItem = value;
        }
        
        // INTENDED ITEM
        private IItemInfo _intendedItem;
        IItemInfo IIntendedCurrentItemGetter.Item => _intendedItem;
        IItemInfo IIntendedCurrentItemSetter.Item
        {
            set => _intendedItem = value;
        }

        // IS ITEM USABLE
        private bool _isUsable;
        bool ICurrentItemUsableGetter.IsUsable => _isUsable;
        bool ICurrentItemUsableSetter.IsUsable
        {
            set => _isUsable = value;
        }
    }
}
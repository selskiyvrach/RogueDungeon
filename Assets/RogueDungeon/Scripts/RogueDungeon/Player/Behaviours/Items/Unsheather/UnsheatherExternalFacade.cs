using Common.Behaviours;
using RogueDungeon.Items.Data.Common;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class UnsheatherExternalFacade : IBehaviourExternalFacade, ICurrentItemGetter, ICurrentItemUsableGetter, IIntendedCurrentItemSetter
    {
        private readonly ICurrentItemGetter _currentItemGetter;
        private readonly ICurrentItemUsableGetter _isUsableGetter;
        private readonly IIntendedCurrentItemSetter _intendedItemSetter;

        public UnsheatherExternalFacade(ICurrentItemGetter currentItemGetter, ICurrentItemUsableGetter isUsableGetter, IIntendedCurrentItemSetter intendedItemSetter)
        {
            _currentItemGetter = currentItemGetter;
            _isUsableGetter = isUsableGetter;
            _intendedItemSetter = intendedItemSetter;
        }

        IItemInfo ICurrentItemGetter.Item => _currentItemGetter.Item;
        bool ICurrentItemUsableGetter.IsUsable => _isUsableGetter.IsUsable;

        IItemInfo IIntendedCurrentItemSetter.Item
        {
            set => _intendedItemSetter.Item = value;
        }
    }
}
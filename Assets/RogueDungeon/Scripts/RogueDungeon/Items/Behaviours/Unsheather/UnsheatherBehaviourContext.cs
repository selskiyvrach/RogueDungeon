using RogueDungeon.Items.Behaviours.Common;
using RogueDungeon.Items.Data.Common;
using UniRx;

namespace RogueDungeon.Items.Behaviours.Unsheather
{
    public class UnsheatherBehaviourContext : ICurrentItemSetter, ICurrentItemGetter, IIntendedItemSetter, IIntendedItemGetter, ICurrentItemUsableSetter, ICurrentItemUsableGetter
    {
        private IItemInfo _intendedItem;
        private IItemInfo _currentItem;
        private readonly ReactiveProperty<bool> _isUsable = new(false);
        IItemInfo ICurrentItemGetter.Item => _currentItem;
        IItemInfo ICurrentItemSetter.Item { set => _currentItem = value; }
        IItemInfo IIntendedItemGetter.Item => _intendedItem;
        IItemInfo IIntendedItemSetter.Item { set => _intendedItem = value; }
        void ICurrentItemUsableSetter.SetUsable(bool value) => _isUsable.Value = value;
        IReadOnlyReactiveProperty<bool> ICurrentItemUsableGetter.IsUsable => _isUsable;
    }
}
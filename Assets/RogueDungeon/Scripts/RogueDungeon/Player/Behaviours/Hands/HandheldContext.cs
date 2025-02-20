using System;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public interface IHandheldContext
    {
        Item CurrentItem { get; set; }
        Item IntendedItem { get; set; }
        void SetCurrentItemInteractable(bool value);
    }
    
    /// <summary>
    /// Serves no other purpose but to remove circular dependency hands->moveset->sheath/unsheath moves->(hands)IHandHeldContext
    /// </summary>
    public class HandHeldContext : IHandheldContext
    {
        private Func<Item> _currentItemGetter;
        private Func<Item> _intendedItemGetter;
        private Action<Item> _currentItemSetter;
        private Action<Item> _intendedItemSetter;
        private Action<bool> _currentItemMoveSetActiveSetter;
        
        public Item CurrentItem { get => _currentItemGetter.Invoke(); set => _currentItemSetter.Invoke(value); }
        public Item IntendedItem { get => _intendedItemGetter.Invoke(); set => _intendedItemSetter.Invoke(value); }

        public bool Inited { get; private set; }

        public void Init(Func<Item> currentItemGetter, Func<Item> intendedItemGetter, Action<Item> currentItemSetter, Action<Item> intendedItemSetter, Action<bool> currentItemMoveSetActiveSetter)
        {
            _currentItemGetter = currentItemGetter;
            _intendedItemGetter = intendedItemGetter;
            _currentItemSetter = currentItemSetter;
            _intendedItemSetter = intendedItemSetter;
            _currentItemMoveSetActiveSetter = currentItemMoveSetActiveSetter;
            Inited = true;
        }

        public void SetCurrentItemInteractable(bool value) => _currentItemMoveSetActiveSetter.Invoke(value);
    }
}
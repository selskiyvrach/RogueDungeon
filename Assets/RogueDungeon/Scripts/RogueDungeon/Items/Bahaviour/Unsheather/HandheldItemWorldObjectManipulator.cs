using RogueDungeon.Items.Handling.Common;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items
{
    public class HandheldItem : MonoBehaviour, IHandheldItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private ICurrentHandheldItemProvider _currentItem;
        private ICurrentItemEnabledState _itemEnabledState;

        [Inject]
        public void Construct(ICurrentHandheldItemProvider currentItem, ICurrentItemEnabledState callbacks)
        {
            _itemEnabledState = callbacks;
            _currentItem = currentItem;
            _spriteRenderer.sprite = _currentItem.ItemInfo.Sprite;
        }

        public void SetVisible(bool value) => 
            gameObject.SetActive(value);

        public void SetCanBeUsed(bool value) => 
            _itemEnabledState.SetEnabled(value);
    }
}
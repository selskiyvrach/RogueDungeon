using UnityEngine;
using Zenject;

namespace RogueDungeon.Items
{
    public class HandheldItem : MonoBehaviour, IHandheldItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public IItem Item { get; private set; }
        
        public void Setup(IItem item)
        {
            Item = item;
            _spriteRenderer.sprite = Item?.Sprite;
        }

        public void SetVisible(bool value) => 
            gameObject.SetActive(value);

        public void SetEnabled(bool value)
        {
            if(value)
                Item.Enable();
            else
                Item.Disable();
        }
    }
}
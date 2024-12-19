using RogueDungeon.Items.Bahaviour.Common;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    public class CurrentItemVisibleSetter : MonoBehaviour, ICurrentItemVisibleSetter
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Inject]
        public void Construct(ICurrentItemGetter currentItemGetter) => 
            _spriteRenderer.sprite = currentItemGetter.Item.Sprite;

        public void SetVisible(bool value) => 
            gameObject.SetActive(value);
    }
}
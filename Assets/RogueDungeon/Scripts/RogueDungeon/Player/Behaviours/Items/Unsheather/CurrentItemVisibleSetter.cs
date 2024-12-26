using RogueDungeon.Items.Behaviours.Common;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items.Behaviours.Unsheather
{
    public class CurrentItemVisibleSetter : MonoBehaviour, ICurrentItemVisibleSetter
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private ICurrentItemGetter _currentItemGetter;

        [Inject]
        public void Construct(ICurrentItemGetter currentItemGetter) =>
            _currentItemGetter = currentItemGetter;

        public void SetVisible(bool value)
        {
            _spriteRenderer.sprite = _currentItemGetter.Item.Sprite;
            gameObject.SetActive(value);
        }
    }
}
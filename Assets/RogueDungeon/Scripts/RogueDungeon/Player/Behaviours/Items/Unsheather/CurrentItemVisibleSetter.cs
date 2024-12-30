using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class CurrentItemVisibleSetter : MonoBehaviour, ICurrentItemVisibleSetter
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private ICurrentItemGetter _currentItemGetter;

        public bool IsVisible
        {
            set {
                _spriteRenderer.sprite = _currentItemGetter.Item.Sprite;
                gameObject.SetActive(value);
            }
        }

        [Inject]
        public void Construct(ICurrentItemGetter currentItemGetter) =>
            _currentItemGetter = currentItemGetter;
    }
}
using UnityEngine;

namespace Game.Features.Items.Infrastructure.View
{
    public class HandHeldItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Show(Sprite sprite)
        {
            gameObject.SetActive(true);
            _spriteRenderer.sprite = sprite;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
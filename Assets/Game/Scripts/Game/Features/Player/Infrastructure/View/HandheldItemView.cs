using Game.Features.Player.App.Presenters;
using UnityEngine;

namespace Game.Features.Player.Infrastructure.View
{
    public class HandheldItemView : MonoBehaviour, IHandheldItemView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public void Show(Sprite sprite)
        {
            gameObject.SetActive(true);
            _spriteRenderer.sprite = sprite;
        }

        public void Hide() => 
            gameObject.SetActive(false);
    }
}
using UnityEngine;

namespace Game.Libs.Items
{
    public class HandItemView : MonoBehaviour
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
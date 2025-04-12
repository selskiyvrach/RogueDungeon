using UnityEngine;

namespace RogueDungeon.Items
{
    public class HandHeldItemPresenter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public bool IsReleased { get; set; } = true;

        public Sprite Sprite { set => _spriteRenderer.sprite = value; }

        private void Awake() => 
            Release();

        public void Release()
        {
            Sprite = null;
            IsReleased = true;
        }
    }
}
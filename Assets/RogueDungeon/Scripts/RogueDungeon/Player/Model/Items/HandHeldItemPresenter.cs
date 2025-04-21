using UnityEngine;
using Zenject;

namespace RogueDungeon.Items
{
    public class HandHeldItemPresenter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Inject]
        public void Construct(IItem item) => 
            _spriteRenderer.sprite = item.Config.Sprite;
    }
}
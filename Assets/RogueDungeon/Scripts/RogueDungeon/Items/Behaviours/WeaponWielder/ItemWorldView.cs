using RogueDungeon.Items.Data.Common;
using UnityEngine;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public class ItemWorldView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetView(IItemInfo info) => 
            _spriteRenderer.sprite = info.Sprite;
    }
}
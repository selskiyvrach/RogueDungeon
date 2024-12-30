using RogueDungeon.Items.Data.Common;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class ItemWorldView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetView(IItemInfo info) => 
            _spriteRenderer.sprite = info.Sprite;
    }
}
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public class ItemInfo : IItemInfo
    {
        public string Id { get; }
        public Sprite Sprite { get; }
        public Vector2Int Size { get; }

        public ItemInfo(string id, Sprite sprite, Vector2Int size)
        {
            Id = id;
            Sprite = sprite;
            Size = size;
        }
    }
}
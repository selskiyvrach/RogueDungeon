using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public class ItemViewSetupArgs : IItemViewSetupArgs
    {
        public string Id { get; }
        public Sprite Sprite { get; }
        public Vector2Int Size { get; }

        public ItemViewSetupArgs(string id, Sprite sprite, Vector2Int size)
        {
            Id = id;
            Sprite = sprite;
            Size = size;
        }
    }
}
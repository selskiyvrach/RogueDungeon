using System;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Libs.Items
{
    public abstract class Item : IItem
    {
        private readonly IItemConfig _itemConfig;
        public string Id { get; }
        public string TypeId => _itemConfig.Id;
        public Vector2Int Size => _itemConfig.Size;

        protected Item(IItemConfig itemConfig, string id)
        {
            _itemConfig = itemConfig;
            Id = id;
        }
    }
}
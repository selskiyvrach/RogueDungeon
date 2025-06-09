using Libs.Movesets;
using UnityEngine;

namespace Game.Libs.Items
{
    public interface IItemConfigsRepository
    {
        IItemConfig GetItemConfig(string itemId);
        IMoveSetConfig GetItemMovesetConfig(string itemId);
        public IHandheldItemViewConfig GetHandheldItemViewConfig(string itemId);
    }
}
using System.Linq;
using Libs.Movesets;
using UnityEngine;

namespace Game.Libs.Items.Configs
{
    public class ItemConfigsRepository : ScriptableObject, IItemConfigsRepository
    {
        [SerializeField] private ItemConfig[] _configs;
        
        public IItemConfig GetItemConfig(string itemId) =>
            _configs.First(n => n.Id == itemId);

        public IMoveSetConfig GetItemMovesetConfig(string itemId) => 
            (IMoveSetConfig)GetItemConfig(itemId);
    }
}
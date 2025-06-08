using System.Linq;
using UnityEngine;

namespace Game.Libs.Items.Configs
{
    public class ItemConfigsRepository : ScriptableObject, IItemConfigsRepository
    {
        [SerializeField] private ItemConfig[] _configs;
        
        public IItemConfig GetItemConfig(string itemId) =>
            _configs.First(n => n.Id == itemId);
    }
}
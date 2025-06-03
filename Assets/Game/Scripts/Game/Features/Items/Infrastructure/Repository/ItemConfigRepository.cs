using System.Linq;
using Game.Features.Items.Domain;
using UnityEngine;

namespace Game.Features.Items.Infrastructure.Repository
{
    public class ItemConfigsRepository : ScriptableObject, IItemConfigsRepository
    {
        [SerializeField] private ItemConfig[] _configs;
        
        public IItemConfig GetItemConfig(string itemId) =>
            _configs.First(n => n.ItemTypeId == itemId);
    }
}
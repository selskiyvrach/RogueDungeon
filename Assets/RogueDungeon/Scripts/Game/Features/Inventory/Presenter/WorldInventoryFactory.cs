using Game.Features.Inventory.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Presenter
{
    public class WorldInventoryFactory : MonoBehaviour, IFactory<InventoryView>
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private InventoryView _prefab;
        
        public InventoryView Create() => 
            Instantiate(_prefab, _parent);
    }
}
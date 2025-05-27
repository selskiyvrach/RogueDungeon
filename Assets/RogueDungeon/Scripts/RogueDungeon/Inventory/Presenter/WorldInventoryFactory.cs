using Inventory.View;
using UnityEngine;
using Zenject;

namespace Inventory.Presenter
{
    public class WorldInventoryFactory : MonoBehaviour, IFactory<InventoryView>
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private InventoryView _prefab;
        
        public InventoryView Create() => 
            Instantiate(_prefab, _parent);
    }
}
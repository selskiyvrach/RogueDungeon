using Game.Features.Player.Domain;

namespace Game.Features.Inventory.App
{
    public class CreateInventoryInstanceOnPlayerSpawnedUseCase
    {
        private readonly IPlayerSpawnedEventDispatcher _playerSpawnedEventDispatcher;

        public CreateInventoryInstanceOnPlayerSpawnedUseCase(IPlayerSpawnedEventDispatcher playerSpawnedEventDispatcher)
        {
            _playerSpawnedEventDispatcher = playerSpawnedEventDispatcher;
            _playerSpawnedEventDispatcher.OnPlayerSpawned += _ => CreateInventory();
        }

        private void CreateInventory()
        {
            // domain spawner.create an inventory
            // cheat set a couple of items
        }
    }
    
    // inventory view installer on player game object
}
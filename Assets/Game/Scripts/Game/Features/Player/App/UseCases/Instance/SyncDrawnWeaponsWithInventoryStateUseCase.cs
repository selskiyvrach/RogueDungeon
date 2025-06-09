using Game.Features.Inventory.Domain;
using Game.Features.Player.Domain.Behaviours.Hands;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class SyncDrawnWeaponsWithInventoryStateUseCase
    {
        private readonly Inventory.Domain.Inventory _inventory;
        private readonly PlayerHandsBehaviour _hands;

        public SyncDrawnWeaponsWithInventoryStateUseCase(Inventory.Domain.Inventory inventory, PlayerHandsBehaviour hands)
        {
            _inventory = inventory;
            _hands = hands;
            _inventory.OnCurrentHandheldItemChanged += ReflectHandContentChange;
            ReflectHandContentChange(Hand.Right);
            ReflectHandContentChange(Hand.Left);
        }

        private void ReflectHandContentChange(Hand obj) => 
            (obj == Hand.Left 
                ? _hands.LeftHand 
                : _hands.RightHand).IntendedItem = _inventory.GetCurrentHandheldItem(obj);
    }
}
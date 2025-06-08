using Game.Libs.Items;

namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public interface IInventory
    {
        IItem GetEquippedItem(bool isRightHand);
        void CycleEquippedItem(bool isRightHand);
    }
}
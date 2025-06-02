public interface IInventory
{
    IItem GetEquippedItem(bool isRightHand);
    void CycleEquippedItem(bool isRightHand);
    IItem GetMapItem();
}
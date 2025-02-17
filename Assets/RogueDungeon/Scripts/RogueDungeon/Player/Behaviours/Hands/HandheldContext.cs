using RogueDungeon.Items;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public interface IHandheldContext
    {
        Item CurrentItem { get; set; }
        Item IntendedItem { get; set; }
        void SetCurrentItemInteractable(bool value);
    }
}
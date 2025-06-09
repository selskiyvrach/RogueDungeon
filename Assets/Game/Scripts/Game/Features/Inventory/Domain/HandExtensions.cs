namespace Game.Features.Inventory.Domain
{
    public static class HandExtensions
    {
        public static CyclableItemsGroup ToCyclableItemsGroup(this Hand hand) =>
            hand switch
            {
                Hand.Right => CyclableItemsGroup.RightHand,
                Hand.Left => CyclableItemsGroup.LeftHand,
                _ => CyclableItemsGroup.None,
            };
    }
}
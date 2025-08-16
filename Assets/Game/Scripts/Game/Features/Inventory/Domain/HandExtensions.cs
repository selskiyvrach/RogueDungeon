namespace Game.Features.Inventory.Domain
{
    public static class HandExtensions
    {
        public static CyclableSlotGroupId ToCyclableItemsGroup(this Hand hand) =>
            hand switch
            {
                Hand.Right => CyclableSlotGroupId.RightHandItems,
                Hand.Left => CyclableSlotGroupId.LeftHandItems,
                _ => CyclableSlotGroupId.None,
            };
        
        public static Hand ToHand(this CyclableSlotGroupId hand) =>
            hand switch
            {
                 CyclableSlotGroupId.RightHandItems => Hand.Right,
                 CyclableSlotGroupId.LeftHandItems=> Hand.Left,
                _ => Hand.None,
            };
    }
}
using System.Collections.Generic;

namespace Game.Features.Inventory.Domain
{
    public class InventoryConfig
    {
        public IEnumerable<SlotId> Slots => new[]
        {
            SlotId.HANDHELD_L_0, 
            SlotId.HANDHELD_L_1,
            SlotId.HANDHELD_L_2,
            SlotId.HANDHELD_R_0, 
            SlotId.HANDHELD_R_1, 
            SlotId.HANDHELD_R_2,
        };
    }
}
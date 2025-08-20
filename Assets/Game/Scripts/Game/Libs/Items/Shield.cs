namespace Game.Libs.Items
{
    public class Shield : BlockingItem, IShield, ISlotableItem
    {
        public SlotCategory SlotCategory => SlotCategory.Handheld;
        public EquipmentType EquipmentType => EquipmentType.Handheld;
        private readonly IShieldItemConfig _config;
        public override BlockingTier BlockingTier => BlockingTier.First;

        public Shield(IShieldItemConfig config, string id) : base(config, id) => 
            _config = config;
    }
}
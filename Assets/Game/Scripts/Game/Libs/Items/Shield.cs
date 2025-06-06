namespace Game.Libs.Items
{
    public class Shield : BlockingItem, IShield
    {
        public EquipmentType EquipmentType => EquipmentType.Handheld;
        private readonly IShieldItemConfig _config;
        public override BlockingTier BlockingTier => BlockingTier.First;

        public Shield(IShieldItemConfig config) : base(config) => 
            _config = config;
    }
}
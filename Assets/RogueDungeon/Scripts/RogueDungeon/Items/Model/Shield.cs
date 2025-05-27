using RogueDungeon.Items.Model.Configs;
using Zenject;

namespace RogueDungeon.Items.Model
{
    public class Shield : HandheldItem, IShield
    {
        public EquipmentType EquipmentType => EquipmentType.Handheld;
        public BlockingItemConfig Config { get; }
        public BlockingTier BlockingTier => BlockingTier.First;
        public float BlockStaminaCostMultiplier => Config.BlockStaminaCostMultiplier;
        
        public Shield(ShieldConfig config) : base(config) => 
            Config = config;
    }

    public interface IShield : IBlockingItem
    {
    }
}
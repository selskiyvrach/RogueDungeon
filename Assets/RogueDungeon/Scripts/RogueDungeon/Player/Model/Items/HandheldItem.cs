using Common.Fsm;

namespace RogueDungeon.Items
{
    public interface IItem
    {
        int InstanceId { get; }
        ItemConfig Config { get; }
    }

    public interface IHandheldItem : IItem
    {
        float UnsheathDuration { get; }
        StateMachine Moveset { get; set; }
        HandHeldItemPresenter Presenter { get; set; }
    }

    public enum EquipmentType
    {
        None,
        Handheld,
        Armor,
        Helmet,
    }

    public interface IBlockingItem : IItem
    {
        public BlockingTier BlockingTier { get; }
        public float BlockStaminaCostMultiplier { get; }
    }

    // if two items equipped -> pick the one with the highest tier or the left one if both are the same
    public enum BlockingTier
    {
        None,
        Second, 
        First, 
    }

    public class HandheldItem : IHandheldItem
    {
        private static int _idCounter;
        public int InstanceId { get; }
        public ItemConfig Config { get; }
        public float UnsheathDuration => Config.UnsheathDuration;
        public HandHeldItemPresenter Presenter { get; set; }
        public StateMachine Moveset { get; set; }

        protected HandheldItem(ItemConfig config)
        {
            Config = config;
            InstanceId = _idCounter++;
        }
    }
}
namespace Game.Libs.Items
{
    public class HandheldItem : Item, IHandheldItem
    {
        private readonly IHandheldItemConfig _config;
        public float UnsheathDuration => _config.UnsheathDuration;
        public float IdleAnimationDuration => _config.IdleAnimationDuration;

        protected HandheldItem(IHandheldItemConfig config, string id) : base(config, id) => 
            _config = config;
    }
}
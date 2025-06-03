namespace Game.Features.Items.Domain
{
    public class HandheldItem : Item, IHandheldItem
    {
        private readonly IHandheldItemConfig _config;
        public float UnsheathDuration => _config.UnsheathDuration;
        public float IdleAnimationDuration => _config.IdleAnimationDuration;

        protected HandheldItem(IHandheldItemConfig config) : base(config) => 
            _config = config;
    }
}
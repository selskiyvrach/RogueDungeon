using Game.Features.Items.Domain.Configs;

namespace Game.Features.Items.Domain
{
    public class HandheldItem : Item, IHandheldItem
    {
        public float UnsheathDuration => Config.UnsheathDuration;

        protected HandheldItem(ItemConfig config) : base(config)
        {
        }
    }
}
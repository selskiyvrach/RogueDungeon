using RogueDungeon.Items.Model.Configs;

namespace RogueDungeon.Items.Model
{
    public class HandheldItem : Item, IHandheldItem
    {
        public float UnsheathDuration => Config.UnsheathDuration;

        protected HandheldItem(ItemConfig config) : base(config)
        {
        }
    }
}
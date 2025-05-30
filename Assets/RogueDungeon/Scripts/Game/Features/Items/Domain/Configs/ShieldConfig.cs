using System;

namespace Game.Features.Items.Domain.Configs
{
    public class ShieldConfig : BlockingItemConfig
    {
        public override Type ItemType => typeof(Shield);
    }
}
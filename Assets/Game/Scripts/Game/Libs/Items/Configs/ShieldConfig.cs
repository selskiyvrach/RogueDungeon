using System;

namespace Game.Libs.Items.Configs
{
    public class ShieldConfig : BlockingItemConfig, IShieldItemConfig
    {
        public override Type Type => typeof(Shield);
    }
}
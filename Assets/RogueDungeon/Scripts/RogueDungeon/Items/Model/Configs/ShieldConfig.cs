using System;

namespace RogueDungeon.Items.Model.Configs
{
    public class ShieldConfig : BlockingItemConfig
    {
        public override Type ItemType => typeof(Shield);
    }
}
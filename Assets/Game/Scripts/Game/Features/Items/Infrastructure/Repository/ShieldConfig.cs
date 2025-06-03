using System;
using Game.Features.Items.Domain;

namespace Game.Features.Items.Infrastructure.Repository
{
    public class ShieldConfig : BlockingItemConfig
    {
        public override Type ItemType => typeof(Shield);
    }
}
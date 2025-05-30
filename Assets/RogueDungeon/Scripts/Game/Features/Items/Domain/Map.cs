using Game.Features.Items.Domain.Configs;

namespace Game.Features.Items.Domain
{
    public class Map : HandheldItem
    {
        private readonly MapItemConfig _config;
        
        public Map(MapItemConfig config) : base(config) => 
            _config = config;
    }
}
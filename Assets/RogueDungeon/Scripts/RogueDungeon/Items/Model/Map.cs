using RogueDungeon.Items.Model.Configs;
using Zenject;

namespace RogueDungeon.Items.Model
{
    public class Map : HandheldItem
    {
        private readonly MapItemConfig _config;
        
        public Map(MapItemConfig config) : base(config) => 
            _config = config;
    }
}
namespace RogueDungeon.Items
{
    public class Map : HandheldItem
    {
        private readonly MapItemConfig _config;
        
        public Map(MapItemConfig config) : base(config) => 
            _config = config;
    }
}
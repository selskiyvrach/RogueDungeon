namespace RogueDungeon.Items
{
    public class Map : Item
    {
        private readonly MapItemConfig _config;
        
        public Map(MapItemConfig config) : base(config) => 
            _config = config;
    }
}
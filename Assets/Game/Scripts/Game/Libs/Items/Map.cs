namespace Game.Libs.Items
{
    public class Map : HandheldItem
    {
        private readonly IMapItemConfig _config;
        public float LowerMapDuration => _config.LowerMapDuration;
        public float RaiseMapDuration => _config.RaiseMapDuration;

        public Map(IMapItemConfig config, string id) : base(config, id) => 
            _config = config;
    }
}
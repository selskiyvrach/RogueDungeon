namespace Game.Libs.Items
{
    public interface IMapItemConfig : IHandheldItemConfig
    {
        float LowerMapDuration { get; }
        float RaiseMapDuration { get; }
    }
}
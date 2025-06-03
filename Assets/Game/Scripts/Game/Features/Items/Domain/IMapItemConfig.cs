namespace Game.Features.Items.Domain
{
    public interface IMapItemConfig : IHandheldItemConfig
    {
        float LowerMapDuration { get; }
        float RaiseMapDuration { get; }
    }
}
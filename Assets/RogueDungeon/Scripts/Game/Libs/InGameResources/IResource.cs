namespace Game.Libs.InGameResources
{
    public interface IResource : IReadOnlyResource
    {
        void AddDelta(float value);
    }
}
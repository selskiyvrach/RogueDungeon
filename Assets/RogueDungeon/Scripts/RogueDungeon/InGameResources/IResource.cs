namespace InGameResources
{
    public interface IResource : IReadOnlyResource
    {
        void AddDelta(float value);
    }
}
namespace Characters
{
    public interface IResource : IReadOnlyResource
    {
        void AddDelta(float value);
    }
}
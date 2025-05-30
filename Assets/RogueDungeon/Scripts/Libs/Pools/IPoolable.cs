namespace Libs.Pools
{
    public interface IPoolable
    {
        int RecyclesCount { get; set; }
        void Reset();
    }
}
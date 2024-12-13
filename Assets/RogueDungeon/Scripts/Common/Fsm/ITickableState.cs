namespace Common.Fsm
{
    public interface ITickableState
    {
        void Tick(float timeDelta);
    }
}
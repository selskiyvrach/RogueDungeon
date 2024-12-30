namespace Common.Behaviours
{
    public interface IBehaviour
    {
        bool IsEnabled { get; }
        void Enable();
        void Disable();
    }
}
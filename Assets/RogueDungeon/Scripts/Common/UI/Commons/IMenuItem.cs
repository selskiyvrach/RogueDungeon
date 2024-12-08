using Common.Commands;

namespace Common.UI.Commons
{
    public interface IMenuItem
    {
        ICommand Command { get; }
        string DisplayName { get; }
        bool IsAvailable { get; }
    }
}
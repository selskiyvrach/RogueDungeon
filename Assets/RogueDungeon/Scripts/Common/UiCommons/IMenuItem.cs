using Common.Commands;

namespace Common.UiCommons
{
    public interface IMenuItem
    {
        ICommand Command { get; }
        string DisplayName { get; }
        bool IsAvailable { get; }
    }
}
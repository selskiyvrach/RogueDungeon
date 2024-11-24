using Common.Commands;

namespace Common.UiCommons
{
    public interface IButton
    {
        ICommand Command { set; }
    }
}
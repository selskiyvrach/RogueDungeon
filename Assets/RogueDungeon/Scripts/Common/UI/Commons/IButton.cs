using Common.Commands;

namespace Common.UI.Commons
{
    public interface IButton
    {
        ICommand Command { set; }
    }
}
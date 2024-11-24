using RogueDungeon.Services.Commands;

namespace RogueDungeon.UI.Common
{
    public interface IMenuItem
    {
        ICommand Command { get; }
        string DisplayName { get; }
        bool IsAvailable { get; }
    }
}
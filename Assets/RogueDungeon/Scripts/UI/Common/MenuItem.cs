using RogueDungeon.Services.Commands;

namespace RogueDungeon.UI.Common
{
    public class MenuItemData : IMenuItem
    {
        public ICommand Command { get; }
        public string DisplayName { get; }
        public bool IsAvailable { get; }

        public MenuItemData(ICommand command, string displayName = null, bool isAvailable = true)
        {
            Command = command;
            DisplayName = displayName;
            IsAvailable = isAvailable;
        }
    }
}
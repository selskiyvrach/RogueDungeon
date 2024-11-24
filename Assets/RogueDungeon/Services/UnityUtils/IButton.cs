using RogueDungeon.Services.Commands;

namespace RogueDungeon.Services.UnityUtils
{
    public interface IButton
    {
        ICommand Command { set; }
    }
}
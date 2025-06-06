using Game.Libs.Input;
using Game.Libs.Items;

namespace Game.Features.Player.Domain.Movesets.Items.Interfaces
{
    public interface IItemInputKeyProvider
    {
        InputKey GetInputKeyForItem(IItem item);
    }
}
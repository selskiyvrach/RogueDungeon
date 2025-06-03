using Game.Libs.Input;

namespace Game.Features.Items.Domain.Wielder
{
    public interface IItemInputKeyProvider
    {
        InputKey GetInputKeyForItem(IItem item);
    }
}
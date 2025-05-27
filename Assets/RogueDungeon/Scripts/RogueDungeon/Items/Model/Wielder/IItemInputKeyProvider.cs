using Input;

namespace RogueDungeon.Items.Model
{
    public interface IItemInputKeyProvider
    {
        InputKey GetInputKeyForItem(IItem item);
    }
}
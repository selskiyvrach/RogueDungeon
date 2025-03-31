using UniRx;

namespace Common.UI
{
    public interface IHideableUiElement : IUiElementViewModel
    {
        IReadOnlyReactiveProperty<bool> IsVisible { get; }
    }
}
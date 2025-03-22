using UniRx;

namespace Common.UI.Bars
{
    public interface IBarViewModel : IUiElementViewModel
    {
        IReadOnlyReactiveProperty<float> Value { get; }
    }
}
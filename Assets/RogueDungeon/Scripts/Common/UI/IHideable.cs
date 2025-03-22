using UniRx;

namespace Common.UI
{
    public interface IHideable
    {
        IReadOnlyReactiveProperty<bool> IsHidden { get; }
    }
}
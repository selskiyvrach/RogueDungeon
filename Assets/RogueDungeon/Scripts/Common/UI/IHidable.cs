using UniRx;

namespace Common.UI
{
    public interface IHidable
    {
        IReadOnlyReactiveProperty<bool> IsHidden { get; }
    }
}
using System;
using UniRx;

namespace Common.UI.Bars
{
    public interface IBarViewModel : IDisposable
    {
        IReadOnlyReactiveProperty<float> Value { get; }
        IReadOnlyReactiveProperty<bool> IsVisible { get; }
    }
}
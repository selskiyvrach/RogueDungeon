using System;
using Common.Mvvm.Model;
using UniRx;

namespace Common.Mvvm.ViewModel
{
    public interface IViewModel : IDisposable
    {
        IReadOnlyReactiveProperty<bool> ShouldRemainOpen { get; }
    }

    public interface IViewModel<T> : IViewModel where T : IModel
    {
    }
}
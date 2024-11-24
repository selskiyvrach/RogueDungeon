using System;
using Common.Mvvm.Model;

namespace Common.Mvvm.ViewModel
{
    public interface IViewModel : IDisposable
    {
    }

    public interface IViewModel<T> : IViewModel where T : IModel
    {
    }
}
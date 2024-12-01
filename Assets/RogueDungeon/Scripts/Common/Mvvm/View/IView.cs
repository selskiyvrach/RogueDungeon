using System;
using Common.Mvvm.ViewModel;

namespace Common.Mvvm.View
{
    public interface IView : IDisposable 
    {
    }

    public interface IView<in T> : IView, IConstructable<T> where T : IViewModel 
    {
        
    }
}
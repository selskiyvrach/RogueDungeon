using System;
using RogueDungeon.Game;

namespace RogueDungeon.UI.Common
{
    public abstract class ViewModel : IViewModel
    {
        public event Action OnDisposed;

        public virtual void Dispose() => 
            OnDisposed?.Invoke();
    }

    public abstract class ViewModel<T> : ViewModel, IViewModel<T> where T : IModel
    {
        protected ViewModel(T model) => 
            model.OnDisposed += Dispose;
    }
}
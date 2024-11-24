using System;
using Common.Mvvm.ViewModel;
using UniRx;
using UnityEngine;

namespace Common.Mvvm.View
{
    public abstract class View<T> : MonoBehaviour, IView<T> where T : IViewModel
    {
        private IDisposable _sub;

        public virtual void Initialize(T viewModel) => 
            _sub = viewModel.ShouldRemainOpen.Subscribe(ChangeOpenState);

        private void ChangeOpenState(bool value)
        {
            if(!value)
                Close();
        }

        protected virtual void Close() => 
            Destroy(gameObject);

        public virtual void Dispose() => 
            _sub?.Dispose();
    }
}
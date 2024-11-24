using Common.Mvvm.ViewModel;
using UnityEngine;

namespace Common.Mvvm.View
{
    public abstract class View<T> : MonoBehaviour, IView<T> where T : IViewModel
    {
        public virtual void Dispose() => 
            Destroy(gameObject);

        public virtual void Initialize(T viewModel)
        {
            
        }
    }
}
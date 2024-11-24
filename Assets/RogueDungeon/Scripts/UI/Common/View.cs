using UnityEngine;

namespace RogueDungeon.UI.Common
{
    public abstract class View<T> : MonoBehaviour, IView<T> where T : IViewModel
    {
        public virtual void Dispose() => 
            Destroy(gameObject);

        public virtual void Initialize(T viewModel) => 
            viewModel.OnDisposed += Dispose;
    }
}
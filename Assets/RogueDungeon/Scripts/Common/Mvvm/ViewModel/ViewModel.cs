using Common.Mvvm.Model;

namespace Common.Mvvm.ViewModel
{
    public abstract class ViewModel : IViewModel
    {
        public virtual void Dispose()
        {
            
        }
    }

    public abstract class ViewModel<T> : ViewModel, IViewModel<T> where T : IModel
    {

    }
}
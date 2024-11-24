using Common.Commands;
using Common.Mvvm.Model;
using UniRx;

namespace Common.Mvvm.ViewModel
{
    public abstract class ViewModel : IViewModel
    {
        private readonly ReactiveProperty<bool> _shouldRemainOpen = new(true);
        public IReadOnlyReactiveProperty<bool> ShouldRemainOpen => _shouldRemainOpen;

        public virtual void Dispose()
        {
        }

        protected void CloseView() => 
            _shouldRemainOpen.Value = false;

        protected ICommand WrapInCloseViewAndDisposeSelfCommand(ICommand command) =>
            new ActionCommand(() =>
            {
                command.Execute();
                CloseView();
                Dispose();
            });
    }

    public abstract class ViewModel<T> : ViewModel, IViewModel<T> where T : IModel
    {

    }
}
using Common.Mvvm.ViewModel;

namespace Common.Mvvm.View
{
    public interface IInitializable<in T> where T : IViewModel
    {
        void Initialize(T viewModel);
    }
}
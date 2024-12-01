using Common.Mvvm.ViewModel;

namespace Common.Mvvm.View
{
    public interface IConstructable<in T> where T : IViewModel
    {
        void Construct(T viewModel);
    }
}
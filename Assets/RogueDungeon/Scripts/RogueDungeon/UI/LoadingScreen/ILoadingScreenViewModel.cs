using Common.Mvvm.ViewModel;
using UniRx;

namespace RogueDungeon.UI.LoadingScreen
{
    public interface ILoadingScreenViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<float> Progress { get; }
        IReadOnlyReactiveProperty<bool> IsFinished { get; }
    }
}
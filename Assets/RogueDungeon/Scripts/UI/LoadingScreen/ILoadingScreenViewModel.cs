using UniRx;

namespace RogueDungeon.UI.LoadingScreen
{
    public interface ILoadingScreenViewModel
    {
        IReadOnlyReactiveProperty<float> Progress { get; }
        IReadOnlyReactiveProperty<bool> IsFinished { get; }
    }
}
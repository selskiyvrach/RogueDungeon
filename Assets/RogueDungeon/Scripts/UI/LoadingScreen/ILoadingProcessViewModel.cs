using RogueDungeon.UI.Common;
using UniRx;

namespace RogueDungeon.UI.LoadingScreen
{
    public interface ILoadingProcessViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<float> Progress { get; }
        IReadOnlyReactiveProperty<bool> IsFinished { get; }
    }
}
using Common.Mvvm.ViewModel;
using Common.SceneManagement;
using UniRx;

namespace RogueDungeon.UI.LoadingScreen
{
    public class LoadingScreenViewModel : ViewModel<ISceneLoadingModel>, ILoadingScreenViewModel
    {
        private readonly ISceneLoadingModel _model;
        private readonly ReactiveProperty<bool> _isFinished = new();
        public IReadOnlyReactiveProperty<float> Progress => _model.Progress;
        public IReadOnlyReactiveProperty<bool> IsFinished => _isFinished;

        public LoadingScreenViewModel(ISceneLoadingModel model)
        {
            _model = model;
            _model.OnFinished += () =>
            {
                _isFinished.Value = true;
                CloseView();
            };
        }
    }
}
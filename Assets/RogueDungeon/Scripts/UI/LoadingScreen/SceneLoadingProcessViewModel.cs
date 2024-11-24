using RogueDungeon.SceneManagement;
using RogueDungeon.UI.Common;
using UniRx;

namespace RogueDungeon.UI.LoadingScreen
{
    public class SceneLoadingProcessViewModel : ViewModel<ISceneLoadingModel>, ILoadingProcessViewModel
    {
        private readonly ISceneLoadingModel _model;
        private readonly ReactiveProperty<bool> _isFinished = new();
        public IReadOnlyReactiveProperty<float> Progress => _model.Progress;
        public IReadOnlyReactiveProperty<bool> IsFinished => _isFinished;

        public SceneLoadingProcessViewModel(ISceneLoadingModel model) : base(model)
        {
            _model = model;
            _model.OnFinished += () => _isFinished.Value = true;
        }
    }
}
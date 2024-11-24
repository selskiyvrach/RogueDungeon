using RogueDungeon.SceneManagement;
using RogueDungeon.UI.Common;
using UniRx;

namespace RogueDungeon.UI.LoadingScreen
{
    public class LoadingProcessViewModel : ViewModel, ILoadingProcessViewModel
    {
        private readonly ILoadingModel _model;
        private readonly ReactiveProperty<bool> _isFinished = new();
        public IReadOnlyReactiveProperty<float> Progress => _model.Progress;
        public IReadOnlyReactiveProperty<bool> IsFinished => _isFinished;

        public LoadingProcessViewModel(ILoadingModel model)
        {
            _model = model;
            _model.OnFinished += () => _isFinished.Value = true;
        }

        public override void Dispose()
        {
            base.Dispose();
            _model.Dispose();
        }
    }
}
using System.Threading.Tasks;
using Zenject;

namespace Common.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IFactory<ISceneLoadingModel> _factory;
        private bool _isFinished;

        public SceneLoader(IFactory<ISceneLoadingModel> factory) => 
            _factory = factory;

        public async Task Load<T>() where T : Scene, new()
        {
            _isFinished = false;
            var model =_factory.Create();
            model.OnFinished += () =>
            {
                _isFinished = true;
                model.Dispose();
            };
            model.Load(new T().SceneName);
            
            while (!_isFinished) 
                await Task.Yield();
        }
    }
}
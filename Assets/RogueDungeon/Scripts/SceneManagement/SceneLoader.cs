using Zenject;

namespace RogueDungeon.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IFactory<ISceneLoadingModel> _factory;

        public SceneLoader(IFactory<ISceneLoadingModel> factory) => 
            _factory = factory;

        public void Load<T>() where T : Scene, new()
        {
            var model =_factory.Create();
            model.OnFinished += model.Dispose;
            model.Load(new T().SceneName);
        }
    }
}
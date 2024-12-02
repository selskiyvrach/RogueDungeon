using System.Threading.Tasks;
using Common.SceneManagement;

namespace Common.Game
{
    public abstract class GameState
    {
        public abstract Task Enter();
    }

    public abstract class GameState<TScene> : GameState where TScene : Scene, new()
    {
        private readonly ISceneLoader _sceneLoader;

        protected GameState(ISceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public override async Task Enter() => 
            await _sceneLoader.Load<TScene>();
    }
}
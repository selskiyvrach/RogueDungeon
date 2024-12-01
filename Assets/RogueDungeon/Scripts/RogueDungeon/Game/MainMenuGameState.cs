using Common.Game;
using Common.SceneManagement;
using RogueDungeon.SceneManagement;

namespace RogueDungeon.Game
{
    public class MainMenuGameState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;

        public MainMenuGameState(ISceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public async void Enter() => 
            await _sceneLoader.Load<MainMenuScene>();
    }
}
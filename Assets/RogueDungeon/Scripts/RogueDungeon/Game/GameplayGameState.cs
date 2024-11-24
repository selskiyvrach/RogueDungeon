using Common.Game;
using Common.SceneManagement;
using RogueDungeon.SceneManagement;

namespace RogueDungeon.Game
{
    internal class GameplayGameState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;

        public GameplayGameState(ISceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public async void Enter()
        {
            await _sceneLoader.Load<GameplayScene>();
            
        }
    }
}
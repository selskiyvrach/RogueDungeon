using RogueDungeon.SceneManagement;

namespace RogueDungeon.Game
{
    public class MainMenuGameState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateChanger _stateChanger;

        public MainMenuGameState(ISceneLoader sceneLoader, IGameStateChanger stateChanger)
        {
            _sceneLoader = sceneLoader;
            _stateChanger = stateChanger;
        }

        public void Enter() => 
            _sceneLoader.LoadScene<MainMenuScene>();
    }
}
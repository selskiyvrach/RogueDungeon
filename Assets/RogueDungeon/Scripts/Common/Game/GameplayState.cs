using Common.SceneManagement;

namespace Common.Game
{
    public class GameplayState : GameState<GameplayScene>
    {
        public GameplayState(ISceneLoader sceneLoader) : base(sceneLoader)
        {
        }
    }
}
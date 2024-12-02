using Common.SceneManagement;
using RogueDungeon.SceneManagement;

namespace Common.Game
{
    public class GameplayState : GameState<GameplayScene>
    {
        public GameplayState(ISceneLoader sceneLoader) : base(sceneLoader)
        {
        }
    }
}
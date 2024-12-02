using Common.SceneManagement;
using RogueDungeon.SceneManagement;

namespace Common.Game
{
    public class MainMenuState : GameState<MainMenuScene>
    {
        public MainMenuState(ISceneLoader sceneLoader) : base(sceneLoader)
        {
        }
    }
}
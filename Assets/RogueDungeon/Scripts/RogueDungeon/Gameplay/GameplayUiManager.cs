using System.Linq;
using RogueDungeon.Player.View;
using RogueDungeon.UI;
using Screen = Common.UI.Screen;

namespace Gameplay
{
    public class GameplayUiManager
    {
        private readonly UiFactory _uiFactory;
        private readonly UiManagerConfig _uiManagerConfig;

        public GameplayUiManager(UiFactory uiFactory, UiManagerConfig uiManagerConfig)
        {
            _uiFactory = uiFactory;
            _uiManagerConfig = uiManagerConfig;
        }

        public void Initialize() =>
            Show<PlayerHud>();

        public void Show<T>() where T : Screen => 
            _uiFactory.Create(_uiManagerConfig.Screens.First(n => n is T));
    }
}
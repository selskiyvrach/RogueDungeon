using System.Linq;
using Player.View;
using UI;
using Screen = Common.UI.Screen;

namespace Gameplay
{
    public class GameplayUiManager
    {
        private readonly ScreenFactory _screenFactory;
        private readonly UiManagerConfig _uiManagerConfig;

        public GameplayUiManager(ScreenFactory screenFactory, UiManagerConfig uiManagerConfig)
        {
            _screenFactory = screenFactory;
            _uiManagerConfig = uiManagerConfig;
        }

        public void Initialize() =>
            Show<PlayerHudView>();

        public void Show<T>() where T : Screen => 
            _screenFactory.Create(_uiManagerConfig.Screens.First(n => n is T));
    }
}
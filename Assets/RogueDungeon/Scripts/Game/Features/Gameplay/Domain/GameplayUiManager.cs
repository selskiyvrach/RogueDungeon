using Game.Features.Player.View;
using Screen = Libs.UI.Screen;

namespace Game.Features.Gameplay.Domain
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
            Show<PlayerHud>();

        public void Show<T>() where T : Screen => 
            _screenFactory.Create(_uiManagerConfig.Screens.First(n => n is T));
    }
}
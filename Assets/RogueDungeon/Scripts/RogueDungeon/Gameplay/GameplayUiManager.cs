using System.Linq;
using Common.UI;
using RogueDungeon.Player.View;

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

        private void ShowGameOver()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize() => 
            _uiFactory.Create(_uiManagerConfig.Screens.First(n => n is PlayerHud));
    }
}
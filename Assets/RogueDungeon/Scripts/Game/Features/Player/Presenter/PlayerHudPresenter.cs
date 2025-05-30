using Game.Features.Player.Domain;
using Game.Features.Player.View;
using Game.Libs.InGameResources;

namespace Game.Features.Player.Presenter
{
    public class PlayerHudPresenter 
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerHud _playerHudView;
        private ResourceBarPresenter _healthBarPresenter;
        private ResourceBarPresenter _staminaBarPresenter;
        
        public PlayerHudPresenter(PlayerModel playerModel, PlayerHud playerHudView)
        {
            _playerModel = playerModel;
            _playerHudView = playerHudView;
        }

        public void Initialize()
        {
            _healthBarPresenter = new ResourceBarPresenter(_playerModel.Health, _playerHudView.HealthBar);
            _staminaBarPresenter = new ResourceBarPresenter(_playerModel.Stamina, _playerHudView.StaminaBar);
        }
    }
}
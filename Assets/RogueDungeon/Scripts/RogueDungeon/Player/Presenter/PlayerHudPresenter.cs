using InGameResources;
using Player.Model;
using Player.View;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Scripts.RogueDungeon.Player.Presenter
{
    public class PlayerHudPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerHudView _playerHud;
        
        private ResourceBarPresenter _healthBarPresenter;
        private ResourceBarPresenter _staminaBarPresenter;

        [Inject]
        public void Construct(PlayerModel player)
        {
            _healthBarPresenter = new ResourceBarPresenter(player.Health, _playerHud.HealthBar);
            _staminaBarPresenter = new ResourceBarPresenter(player.Stamina, _playerHud.StaminaBar);
        }

        private void OnDestroy()
        {
            _healthBarPresenter.Dispose();
            _staminaBarPresenter.Dispose();
        }
    }
}
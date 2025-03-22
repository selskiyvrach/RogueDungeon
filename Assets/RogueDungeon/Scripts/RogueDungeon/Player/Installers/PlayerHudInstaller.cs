using Common.UI.Bars;
using Player.ViewModel;
using UnityEngine;
using Zenject;

namespace Player.Installers
{
    public class PlayerHudInstaller : MonoInstaller
    {
        [SerializeField] private Bar _playerHealthBar;
        [SerializeField] private Bar _playerStaminaBar;

        public override void InstallBindings()
        {
            Container.Inject(_playerHealthBar, new []{ Container.Instantiate<PlayerHealthBarViewModel>()});
            Container.Inject(_playerStaminaBar, new []{ Container.Instantiate<PlayerStaminaBarViewModel>()});
        }
    }
}
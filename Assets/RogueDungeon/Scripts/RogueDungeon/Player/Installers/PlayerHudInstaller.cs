using Common.UI;
using Common.UI.Bars;
using Player.ViewModel;
using UnityEngine;
using Zenject;

namespace Player.Installers
{
    public class PlayerHudInstaller : MonoBehaviour, IUiRootInstaller
    {
        [SerializeField] private Bar _playerHealthBar;
        [SerializeField] private Bar _playerStaminaBar;

        public void Install(DiContainer container)
        {
            container.Inject(_playerHealthBar, new []{ container.Instantiate<PlayerHealthBarViewModel>()});
            container.Inject(_playerStaminaBar, new []{ container.Instantiate<PlayerStaminaBarViewModel>()});
        }
    }
}
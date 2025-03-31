using Common.UI.Bars;
using Player.ViewModel;
using UnityEngine;
using Zenject;

namespace Player.Installers
{
    public class PlayerHudInstaller : MonoInstaller
    {
        [SerializeField] private Bar _playerHealthBar;
        [SerializeField] private Bar _playerHealthBarDelta;
        
        [SerializeField] private Bar _playerStaminaBar;
        [SerializeField] private Bar _playerStaminaBarDelta;

        public override void InstallBindings()
        {
            var deltaConfig = Container.Resolve<BarDeltaConfig>();
            
            var healthBarViewModel = Container.Instantiate<PlayerHealthBarViewModel>();
            _playerHealthBar.Construct(healthBarViewModel);
            _playerHealthBarDelta.Construct(new BarDeltaViewModel(healthBarViewModel, deltaConfig));
            
            var staminaBarViewModel = Container.Instantiate<PlayerStaminaBarViewModel>();
            _playerStaminaBar.Construct(staminaBarViewModel);
            _playerStaminaBarDelta.Construct(new BarDeltaViewModel(staminaBarViewModel, deltaConfig));
        }
    }
}
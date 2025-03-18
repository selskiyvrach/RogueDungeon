 using Common.UI.Bars;
 using Common.UtilsZenject;
 using RogueDungeon.Scripts.RogueDungeon.UI;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayHudInstaller : MonoInstaller
    {
        [SerializeField] private GameplayHud _gameplayHud;
        
        public void Install(DiContainer container)
        {
            container.NewSingle<IBarViewModel, PlayerHealthBarViewModel>();
            container.Inject(_gameplayHud.PlayerHealthBar, new []{ container.Resolve<IBarViewModel>()});
            container.Unbind<IBarViewModel>();
            
            container.NewSingle<IBarViewModel, PlayerStaminaBarViewModel>();
            container.Inject(_gameplayHud.PlayerStaminaBar, new []{ container.Resolve<IBarViewModel>()});
            container.Unbind<IBarViewModel>();
            
            container.InstanceSingle(_gameplayHud);
        }
    }
}
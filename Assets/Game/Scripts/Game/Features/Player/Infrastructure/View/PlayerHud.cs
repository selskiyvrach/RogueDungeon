using Game.UI.Screens;
using Libs.UI.Bars;
using UnityEngine;
using Screen = Game.UI.Screens.Screen;

namespace Game.Features.Player.Infrastructure.View
{
    public class PlayerHud : Screen
    {
        [SerializeField] private Bar _healthBar;
        [SerializeField] private Bar _staminaBar;
        
        protected override DrawOrder DrawOrder => DrawOrder.PlayerHud;
        public IBar HealthBar => _healthBar;
        public IBar StaminaBar => _staminaBar;
    }
}
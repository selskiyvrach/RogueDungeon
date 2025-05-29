using Common.UI.Bars;
using UI;
using UnityEngine;
using Screen = UI.Screen;

namespace Player.View
{
    public class PlayerHudView : Screen
    {
        [SerializeField] private Bar _healthBar;
        [SerializeField] private Bar _staminaBar;
        
        protected override DrawOrder DrawOrder => DrawOrder.PlayerHud;
        public IBar HealthBar => _healthBar;
        public IBar StaminaBar => _staminaBar;
    }
}
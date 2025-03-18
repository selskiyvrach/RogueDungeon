using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class PlayerStaminaBarViewModel : BarViewModel
    {
        public PlayerStaminaBarViewModel(Player.Player player) : base(player.Stamina)
        {
        }

        protected override void UpdateVisibility() => 
            ((ReactiveProperty<bool>)IsVisible).Value = true;
    }
}
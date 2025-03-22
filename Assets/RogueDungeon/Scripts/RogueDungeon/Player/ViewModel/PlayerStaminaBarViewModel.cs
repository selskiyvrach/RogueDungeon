using RogueDungeon.UI;

namespace Player.ViewModel
{
    public class PlayerStaminaBarViewModel : BarViewModel
    {
        public PlayerStaminaBarViewModel(RogueDungeon.Player.Model.Player player) : base(player.Stamina)
        {
        }
    }
}
using RogueDungeon.UI;
using UniRx;

namespace Player.ViewModel
{
    public class PlayerHealthBarViewModel : BarViewModel
    {
        public PlayerHealthBarViewModel(RogueDungeon.Player.Model.Player player) : base(player.Health)
        {
        }
    }
}
using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class PlayerHealthBarViewModel : BarViewModel
    {
        public PlayerHealthBarViewModel(Player.Player player) : base(player.Health)
        {
        }

        protected override bool GetVisibility() => 
            ((ReactiveProperty<bool>)IsVisible).Value = true;
    }
}
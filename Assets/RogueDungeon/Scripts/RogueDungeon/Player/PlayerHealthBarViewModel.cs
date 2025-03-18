using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class PlayerHealthBarViewModel : HealthBarViewModel
    {
        public PlayerHealthBarViewModel(Player.Player player) : base(player.Health)
        {
        }

        protected override void UpdateVisibility() => 
            ((ReactiveProperty<bool>)IsVisible).Value = true;
    }
}
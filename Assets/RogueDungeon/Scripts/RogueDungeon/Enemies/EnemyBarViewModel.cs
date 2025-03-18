using RogueDungeon.Enemies;
using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class EnemyBarViewModel : BarViewModel
    {
        public EnemyBarViewModel(Enemy enemy) : base(enemy.Health)
        {
        }

        protected override void UpdateVisibility() => 
            ((ReactiveProperty<bool>)IsVisible).Value = Health.Current < Health.Max;
    }
}
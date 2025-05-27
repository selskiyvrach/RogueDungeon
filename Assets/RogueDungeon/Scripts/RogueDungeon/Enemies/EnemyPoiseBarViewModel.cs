using UI;

namespace Enemies
{
    public class EnemyPoiseBarViewModel : BarViewModel
    {
        public EnemyPoiseBarViewModel(Enemy enemy) : base(enemy.Poise)
        {
        }

        protected override float GetValue() => 
            1 - base.GetValue();
    }
}
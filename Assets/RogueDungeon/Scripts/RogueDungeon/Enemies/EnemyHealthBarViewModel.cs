using UI;

namespace Enemies
{
    public class EnemyHealthBarViewModel : BarViewModel
    {
        public EnemyHealthBarViewModel(Enemy enemy) : base(enemy.Health)
        {
        }
    }
}
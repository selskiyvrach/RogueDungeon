using RogueDungeon.Enemies;
using RogueDungeon.UI;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class EnemyHealthBarViewModel : BarViewModel
    {
        public EnemyHealthBarViewModel(Enemy enemy) : base(enemy.Health)
        {
        }
    }
}
using RogueDungeon.Enemies;
using RogueDungeon.UI;
using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class EnemyHealthBarViewModel : BarViewModel
    {
        public EnemyHealthBarViewModel(Enemy enemy) : base(enemy.Health)
        {
        }
    }
}
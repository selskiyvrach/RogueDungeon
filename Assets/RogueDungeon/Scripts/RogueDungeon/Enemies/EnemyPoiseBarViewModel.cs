using RogueDungeon.Enemies;
using RogueDungeon.UI;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class EnemyPoiseBarViewModel : BarViewModel
    {
        public EnemyPoiseBarViewModel(Enemy enemy) : base(enemy.Poise)
        {
        }
    }
}
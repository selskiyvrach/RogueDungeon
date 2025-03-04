using System.Collections.Generic;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindContext
    {
        public List<(Enemy enemy, EnemyPosition destination)> EnemiesToMove { get; } = new();
        public Queue<Enemy> AttackersQueue { get; } = new();
        public float SlackTime { get; set; }
    }
}
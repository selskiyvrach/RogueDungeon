using System.Collections.Generic;
using RogueDungeon.Combat;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindContext
    {
        public List<(Enemy enemy, EnemyPosition destination)> EnemiesToMove { get; } = new();
        public Queue<Enemy> AttackersQueue { get; } = new();
        public float SlackTime { get; set; }
    }
}
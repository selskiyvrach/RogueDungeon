using System.Collections.Generic;
using RogueDungeon.Enemies.Attacks;

namespace RogueDungeon.Enemies
{
    public class EnemyAttacks : List<EnemyAttackAction>
    {
        public EnemyAttacks(IEnumerable<EnemyAttackAction> elements) : base(elements)
        {
        }
    }
}
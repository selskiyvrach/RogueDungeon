using RogueDungeon.Combat;

namespace RogueDungeon.Enemies
{
    public class Enemy : IEnemyCombatant
    {
        public EnemyPosition Position { get; }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
    
    // enemy spawning
    // attack moveset for the axe
}
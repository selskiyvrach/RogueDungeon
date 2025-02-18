using System.Collections.Generic;

namespace RogueDungeon.Combat
{
    public class CombatantsRegistry : ICombatantsRegistry
    {
        private readonly List<IEnemyCombatant> _enemies = new(3);
        public IPlayerCombatant Player { get; private set; }
        public IEnumerable<IEnemyCombatant> Enemies => _enemies;

        public void RegisterPlayer(IPlayerCombatant player) => 
            Player = player;

        public void UnregisterPlayer(IPlayerCombatant player)
        {
            if(player == Player)
                Player = null;
            else
                throw new System.ArgumentException("Player is not registered");
        }

        public void RegisterEnemy(IEnemyCombatant enemy) => 
            _enemies.Add(enemy);

        public void UnregisterEnemy(IEnemyCombatant enemy)
        {
            if(!_enemies.Remove(enemy))
                throw new System.ArgumentException("Enemy is not registered");
        }
    }
}
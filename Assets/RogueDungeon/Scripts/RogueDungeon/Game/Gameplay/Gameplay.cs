using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Player;

namespace RogueDungeon.Game.Gameplay
{
    public class Gameplay : Common.Behaviours.Behaviour
    {
        private readonly ICombatantsRegistry _combatantsRegistry;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly IEnemySpawner _enemySpawner;

        public Gameplay(IPlayerSpawner playerSpawner, ICombatantsRegistry combatantsRegistry, IEnemySpawner enemySpawner)
        {
            _playerSpawner = playerSpawner;
            _combatantsRegistry = combatantsRegistry;
            _enemySpawner = enemySpawner;
        }

        public override void Enable()
        {
            base.Enable();
            _playerSpawner.Spawn();
        }
    }
}
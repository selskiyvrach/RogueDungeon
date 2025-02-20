using RogueDungeon.Camera;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Player;

namespace RogueDungeon.Game.Gameplay
{
    public class Gameplay : Common.Behaviours.Behaviour
    {
        private readonly ICombatantsRegistry _combatantsRegistry;
        private readonly GameplayConfig _config;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IGameCamera _camera;

        public Gameplay(GameplayConfig config, IPlayerSpawner playerSpawner, ICombatantsRegistry combatantsRegistry, IEnemySpawner enemySpawner, IGameCamera camera)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _combatantsRegistry = combatantsRegistry;
            _enemySpawner = enemySpawner;
            _camera = camera;
        }

        public override void Enable()
        {
            base.Enable();
            _playerSpawner.Spawn();
            _camera.Follow = ((Player.Player)_combatantsRegistry.Player).GameObject.CameraReferencePoint;
            _enemySpawner.Spawn(new EnemySpawningArgs(_config.TestEnemy, EnemyPosition.Left));
            _enemySpawner.Spawn(new EnemySpawningArgs(_config.TestEnemy, EnemyPosition.Middle));
        }
    }
}
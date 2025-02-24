using Common.UtilsZenject;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Levels;
using RogueDungeon.Player;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyParent _enemyParent;
        [SerializeField] private Transform _levelRoot;
        [SerializeField] private RoomLocalPositionsConfig _roomLocalPositionsConfig;

        public override void InstallBindings()
        {
            // Level
            Container.InstanceSingle(_roomLocalPositionsConfig);
            Container.InstanceSingle(_levelRoot);
            Container.NewSingle<IFactory<RoomConfig, Room>, RoomFactory>();
            Container.NewSingle<IFactory<LevelConfig, Level>, LevelFactory>();
            
            // Combat
            Container.NewSingleInterfaces<AttacksMediator>();
            Container.NewSingleInterfaces<CombatantsRegistry>();
            
            // Enemies
            Container.InstanceSingle(_enemyParent);
            Container.NewSingleInterfaces<EnemyFactory>();
            Container.NewSingleInterfaces<EnemySpawner>();
            
            // Player
            Container.NewSingleInterfaces<PlayerFactory>();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().FromNew().AsSingle()
                .WithArguments(_playerConfig, _playerTransform);
            
            // Gameplay
            Container.InstanceSingle(_gameplayConfig);
            Container.NewSingle<Gameplay>();
        }

        public override void Start()
        {
            base.Start();
            Container.Resolve<Gameplay>().Enable();
        }
    }
}
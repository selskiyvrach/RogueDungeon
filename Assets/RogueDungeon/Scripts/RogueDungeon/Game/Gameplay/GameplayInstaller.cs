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
        [SerializeField] private EnemyParents _enemyParents;
        [SerializeField] private Transform _levelRoot;

        public override void InstallBindings()
        {
            // Level
            var levelContainer = Container.CreateSubContainer();
            levelContainer.InstanceSingle(_levelRoot);
            levelContainer.NewSingle<IFactory<RoomConfig, Room>, RoomFactory>();
            levelContainer.NewSingle<IFactory<LevelConfig, Level>, LevelFactory>();
            Container.Bind<IFactory<LevelConfig, Level>>().FromMethod(() => levelContainer.Resolve<IFactory<LevelConfig, Level>>()).AsSingle();
            
            // Combat
            Container.NewSingleInterfaces<AttacksMediator>();
            Container.NewSingleInterfaces<CombatantsRegistry>();
            
            // Enemies
            Container.InstanceSingle(_enemyParents);
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
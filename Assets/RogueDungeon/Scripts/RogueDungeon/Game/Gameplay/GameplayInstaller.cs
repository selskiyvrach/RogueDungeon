using Common.UtilsZenject;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Player;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyParents _enemyParents;

        public override void InstallBindings()
        {
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
            Container.NewSingle<Gameplay>();
        }

        public override void Start()
        {
            base.Start();
            Container.Resolve<Gameplay>().Enable();
        }
    }
}
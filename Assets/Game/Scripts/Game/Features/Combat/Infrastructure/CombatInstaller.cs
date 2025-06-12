using Game.Features.Combat.App;
using Game.Features.Combat.Domain;
using Game.Features.Combat.Domain.Enemies;
using Game.Libs.Time;
using UnityEngine;
using Zenject;

namespace Game.Features.Combat.Infrastructure
{
    public class CombatInstaller : MonoInstaller
    {
        [SerializeField] private RoomLocalPositionsConfig _roomLocalPositionsConfig;
        [SerializeField] private CombatConfigsRepository _combatConfigsRepository;
        [SerializeField] private EnemyConfigsRepository _enemyConfigsRepository;
        [SerializeField] private Transform _enemiesParent;
        
        public override void InstallBindings()
        {
            Container.Bind<IBattleFieldFactory>().To<BattleFieldFactory>().AsSingle().WithArguments(new object[] {_enemiesParent});
            Container.Bind<IEnemyConfigsRepository>().FromInstance(_enemyConfigsRepository).AsSingle();
            Container.Bind<IFactory<string, Transform, Enemy>>().To<EnemyFactory>().AsSingle();
            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle().WithArguments(new object[]{ _roomLocalPositionsConfig});
            Container.Bind<AttacksMediator>().AsSingle();
            Container.Bind<HiveMind>().AsSingle();
            Container.Bind<Domain.Combat>().FromMethod(() =>
            {
                var combat = Container.Instantiate<Domain.Combat>(new object[] { _combatConfigsRepository });
                Container.Resolve<IGameTime>().StartTicking(combat, TickOrder.Combat);
                return combat;
            }).AsSingle();
            
            Container.Bind<TryStartCombatOnRoomEnterUseCase>().AsSingle().NonLazy();
        }
    }
}
using Common.UtilsZenject;
using RogueDungeon.Combat;
using RogueDungeon.Player;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.NewSingleInterfaces<AttacksMediator>();
            Container.NewSingleInterfaces<CombatantsRegistry>();
            Container.NewSingleInterfaces<PlayerFactory>();
            Container.Bind<IPlayerSpawner>().To<PlayerSpawner>().FromNew().AsSingle()
                .WithArguments(_playerConfig, _playerTransform);
        }
    }
}
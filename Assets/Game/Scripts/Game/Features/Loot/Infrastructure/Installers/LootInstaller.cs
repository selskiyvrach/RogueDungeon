using Game.Features.Loot.App;
using Game.Features.Loot.Domain;
using Game.Features.Loot.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace Game.Features.Loot.Infrastructure.Installers
{
    public class LootInstaller : MonoInstaller
    {
        [SerializeField] private LootConfigsRepository _configsRepository;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LootConfigsRepository>().FromInstance(_configsRepository).AsSingle();
            Container.BindInterfacesTo<LootManager>().AsSingle();
            
            Container.BindInterfacesTo<DropLootOnCombatFinishedUseCase>().AsSingle().NonLazy();
        }
    }
}
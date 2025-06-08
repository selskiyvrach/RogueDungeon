using Game.Libs.Items.Configs;
using Game.Libs.Items.Factory;
using UnityEngine;
using Zenject;

namespace Game.Libs.Items.Installers
{
    public class ItemsInstaller : MonoInstaller
    {
        [SerializeField] private ItemConfigsRepository _configsRepo;

        public override void InstallBindings()
        {
            Container.Bind<IItemConfigsRepository>().To<ItemConfigsRepository>().FromInstance(_configsRepo).AsSingle();
            Container.Bind<ItemFactory>().AsSingle();
        }
    }
}
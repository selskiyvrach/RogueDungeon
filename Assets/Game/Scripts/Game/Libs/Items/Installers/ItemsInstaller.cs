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
            Container.BindInterfacesTo<ItemConfigsRepository>().FromInstance(_configsRepo).AsSingle();
            Container.BindInterfacesTo<ItemFactory>().AsSingle();
        }
    }
}
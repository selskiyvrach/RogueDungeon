using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Infrastructure.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class ItemViewInstaller : MonoInstaller
    {
        [SerializeField] private ItemView _view;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ItemView>().FromInstance(_view).AsSingle();
            Container.BindInterfacesTo<ItemPresenter>().AsSingle().NonLazy();
        }
    }
}
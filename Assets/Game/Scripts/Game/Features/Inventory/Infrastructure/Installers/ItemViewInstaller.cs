using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Infrastructure.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Installers
{
    public class ItemViewInstaller : MonoInstaller
    {
        [SerializeField] private ItemView _view;
        [SerializeField] private ItemViewAnimator _animator;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ItemViewAnimator>().FromInstance(_animator).AsSingle();
            Container.BindInterfacesTo<ItemView>().FromInstance(_view).AsSingle();
            Container.BindInterfacesTo<ItemPresenter>().AsSingle().NonLazy();
        }
    }
}
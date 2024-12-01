using Common.InstallerGenerator;
using Common.Mvvm.Model;
using Common.Mvvm.View;
using Common.Mvvm.ViewModel;
using Common.UiCommons;
using UnityEngine;
using Zenject;

namespace Common.Mvvm.Zenject
{
    public abstract class MvvmFactory<TModelImpl, TViewModelImpl, TViewImpl, TModelInterface, TFactory> : ScriptableInstaller, IFactory<TModelInterface> 
        where TModelInterface : IModel where TModelImpl : TModelInterface
        where TViewModelImpl : IViewModel<TModelInterface>
        where TViewImpl : MonoBehaviour, IView<TViewModelImpl>
        where TFactory : class, IFactory<TModelInterface>
    {
        [SerializeField] private TViewImpl _viewPrefab;
        
        private DiContainer _diContainer;
        private IUiRootObject _uiRootObject;

        public override void Install(DiContainer container)
        {
            base.Install(container);
            _diContainer = container;
            _uiRootObject = container.Resolve<IUiRootObject>();
            
            // need to bind both tfactory and ifactory<TInterface>
            container.BindInterfacesAndSelfTo<TFactory>().FromInstance(this as TFactory);
            container.Bind<TModelInterface>().FromFactory<TFactory>();
        }

        public TModelInterface Create()
        {
            var subContainer = _diContainer.CreateSubContainer();

            subContainer.Bind<TModelInterface>().To<TModelImpl>().AsSingle();
            subContainer.Bind<TViewModelImpl>().AsSingle();

            var model = subContainer.Resolve<TModelInterface>();
            var viewModel = subContainer.Resolve<TViewModelImpl>();

            var view = Instantiate(_viewPrefab, _uiRootObject.UiRootTransform);
            view.Initialize(viewModel);

            return (TModelImpl)model;
        }
    }
}
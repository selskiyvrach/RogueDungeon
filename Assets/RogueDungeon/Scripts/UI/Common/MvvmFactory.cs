using RogueDungeon.Game;
using RogueDungeon.UI.Common;
using UnityEngine;
using Zenject;

namespace RogueDungeon.UI.LoadingScreen
{
    public abstract class MvvmFactory<TModelImpl, TViewModelImpl, TViewImpl, TModelInterface> : ScriptableObject, IFactory<TModelInterface> 
        where TModelInterface : IModel where TModelImpl : TModelInterface
        where TViewModelImpl : IViewModel<TModelInterface>
        where TViewImpl : MonoBehaviour, IView<TViewModelImpl>
    {
        [SerializeField] private TViewImpl _viewPrefab;
        
        [Inject] private DiContainer _diContainer;
        [Inject] private IUiRootObject _uiRootObject;

        public TModelInterface Create()
        {
            var subContainer = _diContainer.CreateSubContainer();

            subContainer.Bind<TModelInterface>().To<TModelImpl>().AsSingle();
            subContainer.Bind<TViewModelImpl>().AsSingle();

            var model = subContainer.Resolve<TModelInterface>();
            var viewModel = subContainer.Resolve<TViewModelImpl>();

            var view = Instantiate(_viewPrefab, _uiRootObject.UiRootTransform);
            view.Initialize(viewModel);

            return model;
        }
    }
}
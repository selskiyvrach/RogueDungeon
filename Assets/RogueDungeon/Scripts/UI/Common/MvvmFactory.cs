using RogueDungeon.Game;
using RogueDungeon.UI.Common;
using UnityEngine;
using Zenject;

namespace RogueDungeon.UI.LoadingScreen
{
    public abstract class MvvmFactory<TModelImplementation, TViewModelImplementation, TViewPrefab, TModelInterface> : ScriptableObject, IFactory<TModelInterface> 
        where TModelInterface : IModel where TModelImplementation : TModelInterface
        where TViewModelImplementation : IViewModel<TModelInterface>
        where TViewPrefab : MonoBehaviour, IView<TViewModelImplementation>
    {
        [SerializeField] private TViewPrefab _viewPrefab;
        
        [Inject] private DiContainer _diContainer;
        [Inject] private IUiRootObject _uiRootObject;

        public TModelInterface Create()
        {
            var subContainer = _diContainer.CreateSubContainer();

            subContainer.Bind<TModelInterface>().To<TModelImplementation>().AsSingle();
            subContainer.Bind<TViewModelImplementation>().AsSingle();

            var model = subContainer.Resolve<TModelInterface>();
            var viewModel = subContainer.Resolve<TViewModelImplementation>();

            var view = Instantiate(_viewPrefab, _uiRootObject.UiRootTransform);
            view.Initialize(viewModel);

            return model;
        }
    }
}
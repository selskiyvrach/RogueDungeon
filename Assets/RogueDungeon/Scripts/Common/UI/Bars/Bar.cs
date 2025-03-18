using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Common.UI.Bars
{
    public abstract class Bar : MonoBehaviour
    {
        [field: SerializeField] public Bar _deltaBar;
        
        private IBarViewModel _viewModel;
        private IDisposable _disposables;

        [Inject]
        public virtual void Construct(IBarViewModel viewModel, BarDeltaConfig deltaConfig)
        {
            _viewModel = viewModel;
            _disposables = new CompositeDisposable(_viewModel.Value.Subscribe(SetValue), _viewModel.IsVisible.Subscribe(SetVisibility), _viewModel);
            _deltaBar?.Construct(new BarDeltaViewModel(viewModel, deltaConfig), deltaConfig);
        }
        protected abstract void SetValue(float value);

        protected virtual void SetVisibility(bool isVisible) => 
            gameObject.SetActive(isVisible);

        protected virtual void OnDestroy() => 
            _disposables.Dispose();
    }
}
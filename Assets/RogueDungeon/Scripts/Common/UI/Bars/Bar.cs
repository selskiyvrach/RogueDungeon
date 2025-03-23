using UniRx;
using UnityEngine;
using Zenject;

namespace Common.UI.Bars
{
    public abstract class Bar : UiElement
    {
        [field: SerializeField] public Bar _deltaBar;
        private IBarViewModel _viewModel;

        [Inject]
        public void Construct(IBarViewModel viewModel, BarDeltaConfig deltaConfig)
        {
            _viewModel = viewModel;
            _viewModel.Value.Subscribe(SetValue).AddTo(gameObject);
            _deltaBar?.Construct(new BarDeltaViewModel(viewModel, deltaConfig), deltaConfig);
        }
        
        protected abstract void SetValue(float value);
    }
}
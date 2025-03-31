using UniRx;

namespace Common.UI.Bars
{
    public abstract class Bar : UiElement
    {
        private IBarViewModel ViewModel { get; set; }

        public void Construct(IBarViewModel viewModel)
        {
            base.Construct(viewModel);
            ViewModel = viewModel;
            ViewModel.Value.Subscribe(SetValue).AddTo(gameObject);
        }
        
        protected abstract void SetValue(float value);
    }
}
using Common.UI.Bars;
using RogueDungeon.Characters;
using UniRx;

namespace RogueDungeon.UI
{
    public abstract class BarViewModel : IBarViewModel
    {
        private IReadOnlyResource _resource;
        public IReadOnlyReactiveProperty<float> Value { get; } = new ReactiveProperty<float>();

        protected BarViewModel(IReadOnlyResource resource) => 
            SetResource(resource);

        protected BarViewModel()
        {
        }

        protected void SetResource(IReadOnlyResource resource)
        {
            _resource = resource;
            _resource.OnChanged += UpdateValue;
            UpdateValue();
        }

        private void UpdateValue() => 
            ((ReactiveProperty<float>)Value).Value = GetValue();

        protected virtual float GetValue() => 
            _resource.Current / _resource.Max;

        public void Dispose() => 
            _resource.OnChanged -= UpdateValue;
    }
}
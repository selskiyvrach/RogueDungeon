using Common.UI.Bars;
using RogueDungeon.Characters;
using UniRx;

namespace RogueDungeon.UI
{
    public abstract class BarViewModel : IBarViewModel
    {
        protected readonly IReadOnlyResource Resource;
        public IReadOnlyReactiveProperty<float> Value { get; } = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();

        protected BarViewModel(IReadOnlyResource resource)
        {
            Resource = resource;
            Resource.OnChanged += UpdateValue;
            UpdateValue();
        }

        private void UpdateValue()
        {
            ((ReactiveProperty<float>)Value).Value = GetValue();
            ((ReactiveProperty<bool>)IsVisible).Value = GetVisibility();
        }

        protected virtual float GetValue() => 
            Resource.Current / Resource.Max;

        protected virtual bool GetVisibility() => true;
    }
}
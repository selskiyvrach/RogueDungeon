using Common.UI.Bars;
using RogueDungeon.Characters;
using UniRx;

namespace RogueDungeon.UI
{
    public abstract class BarViewModel : IBarViewModel
    {
        protected readonly IReadOnlyResource Health;
        public IReadOnlyReactiveProperty<float> Value { get; } = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();

        protected BarViewModel(IReadOnlyResource health)
        {
            Health = health;
            Health.OnChanged += UpdateValue;
            UpdateValue();
        }

        public void Dispose() => 
            Health.OnChanged -= UpdateValue;

        private void UpdateValue()
        {
            ((ReactiveProperty<float>)Value).Value = Health.Current / Health.Max;
            ((ReactiveProperty<bool>)IsVisible).Value = GetVisibility();
        }

        protected virtual bool GetVisibility() => true;
    }
}
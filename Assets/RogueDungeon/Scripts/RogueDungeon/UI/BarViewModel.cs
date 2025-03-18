using Characters;
using Common.UI.Bars;
using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public abstract class BarViewModel : IBarViewModel
    {
        protected readonly IResource Health;
        public IReadOnlyReactiveProperty<float> Value { get; } = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();

        protected BarViewModel(IResource health)
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
            UpdateVisibility();
        }

        protected abstract void UpdateVisibility();
    }
}
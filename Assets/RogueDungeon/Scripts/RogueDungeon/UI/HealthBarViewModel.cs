using Characters;
using Common.UI.Bars;
using UniRx;

namespace RogueDungeon.Scripts.RogueDungeon.UI
{
    public class HealthBarViewModel : IBarViewModel
    {
        private readonly IHealth _health;
        public IReadOnlyReactiveProperty<float> Value { get; } = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<bool> IsVisible { get; } = new ReactiveProperty<bool>();

        public HealthBarViewModel(IHealth health)
        {
            _health = health;
            _health.OnChanged += UpdateValue;
            UpdateValue();
        }

        public void Dispose() => 
            _health.OnChanged -= UpdateValue;

        private void UpdateValue()
        {
            ((ReactiveProperty<float>)Value).Value = _health.Current / _health.Max;
            ((ReactiveProperty<bool>)IsVisible).Value = _health.Current < _health.Max;
        }
    }
}
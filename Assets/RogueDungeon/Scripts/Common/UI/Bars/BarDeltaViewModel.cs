using System;
using UniRx;
using UnityEngine;

namespace Common.UI.Bars
{
    public class BarDeltaViewModel : IBarViewModel
    {
        private readonly BarDeltaConfig _config;
        private readonly IBarViewModel _barViewModel;
        private readonly IDisposable _onChangedSub;
        
        private IDisposable _tickSub;
        private float _timeSinceChange;
        private float _previousValue;

        public IReadOnlyReactiveProperty<float> Value { get; } = new ReactiveProperty<float>();
        public IReadOnlyReactiveProperty<bool> IsVisible => _barViewModel.IsVisible;

        public BarDeltaViewModel(IBarViewModel barViewModel, BarDeltaConfig config)
        {
            _barViewModel = barViewModel;
            _config = config;
            ((ReactiveProperty<float>)Value).Value = barViewModel.Value.Value;
            _onChangedSub = _barViewModel.Value.Subscribe(StartCatch);
        }

        private void StartCatch(float value)
        {
            _tickSub?.Dispose();
            if (float.IsNaN(Value.Value))
            {
                
            }
            if (value > Value.Value)
            {
                ((ReactiveProperty<float>)Value).Value = _barViewModel.Value.Value;
                if (float.IsNaN(Value.Value))
                {
                
                }
                return;
            }
            if (float.IsNaN(Value.Value))
            {
                
            }

            _timeSinceChange = 0;
            _previousValue = Value.Value;
            _tickSub = Observable.EveryUpdate().Subscribe(_ => Tick());
        }

        private void Tick()
        {
            _timeSinceChange += Time.deltaTime;
            if(_timeSinceChange <= _config.Delay)
                return;
            var timeSinceDelay = _timeSinceChange - _config.Delay;
            if (float.IsNaN(Value.Value))
            {
                
            }
            ((ReactiveProperty<float>)Value).Value = Mathf.Lerp(_previousValue, _barViewModel.Value.Value, Mathf.Clamp01(timeSinceDelay / _config.CatchDuration));
            if (float.IsNaN(Value.Value))
            {
                
            }
            
            if(timeSinceDelay > _config.CatchDuration)
                _tickSub.Dispose();
        }

        public void Dispose()
        {
            _onChangedSub.Dispose();
            _tickSub?.Dispose();
        }
    }
}
using UnityEngine;
using Zenject;

namespace Libs.UI.Bars
{
    public class BarWithDelta : Bar
    {
        [field: SerializeField] public Bar _valueBar;
        [field: SerializeField] public Bar _deltaBar;

        private BarDeltaConfig _config;
        private float _timeSinceChange;
        private float _deltaValueToCatchFrom;

        public override float Value
        {
            get => _valueBar.Value;
            set
            {
                var prevValue = _valueBar.Value;
                _valueBar.Value = value;
                if(_deltaBar.Value < _valueBar.Value)
                    _deltaValueToCatchFrom = _deltaBar.Value = _valueBar.Value;
                else if(prevValue > _valueBar.Value)
                {
                    _deltaValueToCatchFrom = _deltaBar.Value;
                    _timeSinceChange = 0;
                }
            }
        }

        [Inject]
        private void Construct(BarDeltaConfig config) => 
            _config = config;

        private void Update()
        {
            _timeSinceChange += Time.deltaTime;
            if (_timeSinceChange < _config.Delay)
                return;
            if(_deltaBar.Value > _valueBar.Value)
                _deltaBar.Value = Mathf.Lerp(_deltaValueToCatchFrom, _valueBar.Value, (_timeSinceChange - _config.Delay) / _config.CatchDuration);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Libs.Utils.DotNet;

namespace Game.Libs.Input
{
    public class InputMap
    {
        private readonly InputUnit[] _inputUnits;
        private readonly List<InputUnit> _enabledUnits = new();
        public IEnumerable<InputUnit> EnabledUnits => _enabledUnits;
        public IEnumerable<InputUnit> AllUnits => _inputUnits;

        public InputMap(InputMapConfig config)
        {
            _inputUnits = config.Units.ToArray();
            _enabledUnits.AddRange(_inputUnits);
        }

        public void SetFilter(InputFilter filter)
        {
            _enabledUnits.Where(n => !filter.AllowedKeys.Contains(n.Key)).Foreach(n => n.Reset());
            _enabledUnits.Clear();
            _enabledUnits.AddRange(filter == null
                ? _inputUnits 
                : _inputUnits.Where(n => filter.AllowedKeys.Contains(n.Key)));
        }
    }
}
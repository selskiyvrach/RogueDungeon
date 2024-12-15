using System.Collections.Generic;
using Common.Keys;
using Common.Parameters;

namespace RogueDungeon.Parameters
{
    public class Parameters : IParameters
    {
        private readonly Dictionary<Key, float> _values;

        public Parameters(Dictionary<Key, float> values) => 
            _values = values;

        public float Get(Key key) => 
            _values[key];
    }
}
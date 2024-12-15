using System;
using System.Linq;
using Common.Parameters;

namespace RogueDungeon.Parameters
{
    [Serializable]
    public class ParametersPicker : ParametersPicker<ParameterKeys>
    {
        public Parameters ToParameters() => 
            new(this.ToDictionary(n => (Key)n.Key, n => n.Value));
    }
}
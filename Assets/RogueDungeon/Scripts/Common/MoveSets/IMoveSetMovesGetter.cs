using System.Collections.Generic;

namespace Common.MoveSets
{
    internal interface IMoveSetMovesGetter
    {
        public IEnumerable<IMove> All { get; }
    }
}
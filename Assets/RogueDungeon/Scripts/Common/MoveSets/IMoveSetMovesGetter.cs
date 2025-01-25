using System.Collections.Generic;

namespace Common.MoveSets
{
    internal interface IMoveSetMovesGetter
    {
        public IMove IdleMove { get; }
        public IEnumerable<IMove> All { get; }
    }
}
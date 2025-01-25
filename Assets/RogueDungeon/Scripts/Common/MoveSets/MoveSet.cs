using System.Collections.Generic;

namespace Common.MoveSets
{
    internal class MoveSet : IMoveSetMovesGetter
    {
        private readonly IMove[] _moves;
        public IEnumerable<IMove> All => _moves;

        public MoveSet(IMove[] moves) => 
            _moves = moves;
    }
}
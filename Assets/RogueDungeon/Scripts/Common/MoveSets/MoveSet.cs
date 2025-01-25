using System.Collections.Generic;
using System.Linq;

namespace Common.MoveSets
{
    public class MoveSet : IMoveSetMovesGetter
    {
        private readonly IMove[] _moves;
        public IMove IdleMove { get; }
        public IEnumerable<IMove> All => _moves;

        public MoveSet(IEnumerable<IMove> moves, IMove idleMove)
        {
            IdleMove = idleMove;
            _moves = moves.ToArray();
        }
    }
}
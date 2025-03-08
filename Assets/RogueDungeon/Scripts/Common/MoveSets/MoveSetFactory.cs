using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using Zenject;

namespace Common.MoveSets
{
    public class MoveSetFactory : IFactory<MoveSetConfig, StateMachine>
    {
        private readonly DiContainer _container;

        public MoveSetFactory(DiContainer container) => 
            _container = container;

        public StateMachine Create(MoveSetConfig config) =>
            new(new IdBasedTransitionStrategy(CreateMoves(config.Moves), config.FirstMove.Id));

        private IEnumerable<Move> CreateMoves(IEnumerable<MoveConfig> moveConfigs)
        {
            var moves = moveConfigs.Select(n => _container.Instantiate(n.MoveType, new[] { n, CreateAnimation(n) })).Cast<Move>().ToArray();
            CreateTransitions(moves);
            return moves;
        }

        private object CreateAnimation(MoveConfig n) => 
            _container.Instantiate(n.AnimationConfigPicker.Config.AnimationType, new object[]{n.AnimationConfigPicker.Config});

        private void CreateTransitions(Move[] moves)
        {
            foreach (var move in moves) 
                move.Transitions = move.Config.Transitions.Select(n => new Transition(moves.First(m => m.Id == n.MoveId), n.CanInterrupt)).ToArray();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using Zenject;

namespace Common.MoveSets
{
    public class MoveSetFactory : IFactory<IMoveSetConfig, StateMachine>
    {
        protected readonly DiContainer Container;
        
        public MoveSetFactory(DiContainer container) => 
            Container = container;

        public StateMachine Create(IMoveSetConfig config) =>
            new(new IdBasedTransitionStrategy(CreateMoves(config.MovesCreationArgs), config.FirstMoveId));

        public IEnumerable<Move> CreateMoves(IEnumerable<MoveCreationArgs> moveConfigs)
        {
            var moves = moveConfigs.Select(n => Container.Instantiate(n.MoveType, new []{n.MoveConstructorArgs, CreateAnimation(n)})).Cast<Move>().ToArray();
            CreateTransitions(moves);
            return moves;
        }

        private object CreateAnimation(MoveCreationArgs n) => 
            Container.Instantiate(n.AnimationConfig.AnimationType, new object[]{n.AnimationConfig});

        private void CreateTransitions(Move[] moves)
        {
            foreach (var move in moves) 
                move.Transitions = move.Config.Transitions.Select(n => new Transition(moves.First(m => m.Id == n.MoveId), n.CanInterrupt)).ToArray();
        }
    }
}
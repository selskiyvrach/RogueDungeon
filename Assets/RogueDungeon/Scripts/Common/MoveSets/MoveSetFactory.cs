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

        private IEnumerable<Move> CreateMoves(IEnumerable<MoveCreationArgs> moveConfigs)
        {
            var configs = moveConfigs.ToArray();
            var moves = configs.Select(n => Container.Instantiate(n.MoveType, n.ExtraArguments.Append(n.Id).Append(CreateAnimation(n)))).Cast<Move>().ToArray();
            CreateTransitions(moves, configs);
            return moves;
        }

        private object CreateAnimation(MoveCreationArgs n) => 
            Container.Instantiate(n.AnimationConfig.AnimationType, new object[]{n.AnimationConfig});

        private void CreateTransitions(Move[] moves, MoveCreationArgs[] configs)
        {
            for (var i = 0; i < moves.Length; i++) 
                moves[i].Transitions = configs[i].Transitions.Select(n => new Transition(moves.First(m => m.Id == n.MoveId), n.CanInterrupt)).ToArray();
        }
    }
}
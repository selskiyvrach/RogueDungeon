using System.Collections.Generic;
using System.Linq;
using Libs.Fsm;
using Zenject;

namespace Libs.Movesets
{
    public class MoveSetFactory : IFactory<IMoveSetConfig, StateMachine>
    {
        public DiContainer Container { get; set; }

        public MoveSetFactory(DiContainer container) => 
            Container = container;

        public StateMachine Create(IMoveSetConfig config) =>
            new(new IdBasedTransitionStrategy(CreateMoves(config.MovesCreationArgs), config.FirstMoveId));

        public IEnumerable<Move> CreateMoves(IEnumerable<MoveCreationArgs> moveConfigs)
        {
            var configs = moveConfigs.ToArray();
            var moves = configs.Select(CreateMove).ToArray();
            CreateTransitions(moves, configs);
            return moves;
        }

        public Move CreateMove(MoveCreationArgs n) => 
            (Move)Container.Instantiate(n.MoveType, n.ExtraArguments.Append(n.Id).Append(n.AnimationConfig.Create(Container)));

        private void CreateTransitions(Move[] moves, MoveCreationArgs[] configs)
        {
            for (var i = 0; i < moves.Length; i++) 
                moves[i].Transitions = configs[i].Transitions.Select(n => new Transition(moves.First(m => m.Id == n.MoveId), n.CanInterrupt)).ToArray();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Libs.Fsm;
using Zenject;

namespace Libs.Movesets
{
    public class MoveSetFactory : IFactory<IMoveSetConfig, IMoveIdToTypeConverter, StateMachine>
    {
        public DiContainer Container { get; set; }

        public MoveSetFactory(DiContainer container) => 
            Container = container;

        public StateMachine Create(IMoveSetConfig config, IMoveIdToTypeConverter idToMoveTypeConverter) => 
            new(new IdBasedTransitionStrategy(CreateMoves(config.MovesCreationArgs, idToMoveTypeConverter), config.FirstMoveId));

        private IEnumerable<Move> CreateMoves(IEnumerable<MoveCreationArgs> moveConfigs, IMoveIdToTypeConverter idToMoveTypeConverter)
        {
            var configs = moveConfigs.ToArray();
            var moves = configs.Select(n => CreateMove(n, idToMoveTypeConverter)).ToArray();
            CreateTransitions(moves, configs);
            return moves;
        }

        public Move CreateMove(MoveCreationArgs n, IMoveIdToTypeConverter moveIdConverter) => 
            (Move)Container.Instantiate(moveIdConverter.GetMoveType(n.MoveTypeId), n.AdditionalArgs.Append(n.Id).Append(n.AnimationConfig.Create(Container)));

        private void CreateTransitions(Move[] moves, MoveCreationArgs[] configs)
        {
            for (var i = 0; i < moves.Length; i++) 
                moves[i].Transitions = configs[i].Transitions.Select(n => new Transition(moves.First(m => m.Id == n.MoveId), n.CanInterrupt)).ToArray();
        }
    }
}
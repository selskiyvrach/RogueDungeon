using System.Collections.Generic;
using System.Linq;
using Libs.Fsm;
using Zenject;

namespace Libs.Movesets
{
    public class MoveSetFactory : IFactory<IMoveSetConfig, IMoveIdToTypeConverter, StateMachine>
    {
        private IMoveIdToTypeConverter _moveIdConverter;
        public DiContainer Container { get; set; }

        public MoveSetFactory(DiContainer container) => 
            Container = container;

        public StateMachine Create(IMoveSetConfig config, IMoveIdToTypeConverter idToMoveTypeConverter)
        {
            _moveIdConverter = idToMoveTypeConverter;
            return new StateMachine(new IdBasedTransitionStrategy(CreateMoves(config.MovesCreationArgs), config.FirstMoveId));
        }

        private IEnumerable<Move> CreateMoves(IEnumerable<MoveCreationArgs> moveConfigs)
        {
            var configs = moveConfigs.ToArray();
            var moves = configs.Select(CreateMove).ToArray();
            CreateTransitions(moves, configs);
            return moves;
        }

        public Move CreateMove(MoveCreationArgs n) => 
            (Move)Container.Instantiate(_moveIdConverter.GetMoveType(n.MoveTypeId), new object[] {n.Id}.Append(n.AnimationConfig.Create(Container)));

        private void CreateTransitions(Move[] moves, MoveCreationArgs[] configs)
        {
            for (var i = 0; i < moves.Length; i++) 
                moves[i].Transitions = configs[i].Transitions.Select(n => new Transition(moves.First(m => m.Id == n.MoveId), n.CanInterrupt)).ToArray();
        }
    }
}
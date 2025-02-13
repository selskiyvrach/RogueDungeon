using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using UnityEngine.Assertions;
using Zenject;

namespace RogueDungeon.MoveSets
{
    public class MoveSetFactory : IFactory<MoveSetConfig, DiContainer, MoveSetBehaviour>
    {
        public MoveSetBehaviour Create(MoveSetConfig config, DiContainer container) =>
            new(new StateMachine(CreateTransitionStrategy(config, container)));

        private IStateTransitionStrategy CreateTransitionStrategy(MoveSetConfig config, DiContainer container)
        {
            var moves = CreateMoves(config.Moves, container);
            CreateTransitions(moves);
            var strategy = new IdBasedTransitionStrategy
            {
                StartStateId = config.Moves.First().Id,
                TransitionMap = moves.ToDictionary(n => n.Id, n => (IIdBasedTransitionableState)n)
            };
            return strategy;
        }

        private IEnumerable<Move> CreateMoves(IEnumerable<MoveConfig> moveConfigs, DiContainer container) => 
            moveConfigs.Select(n => container.Instantiate(n.MoveType, new[] { n })).Cast<Move>();

        private void CreateTransitions(IEnumerable<Move> moves)
        {
            foreach (var move in moves)
            {
                Assert.IsTrue(move.Transitions.Count == 0);
                move.Transitions.AddRange(moves.Where(n => move.Config.Transitions.Contains(n.Id)));
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using UnityEngine.Assertions;
using Zenject;

namespace Common.MoveSets
{
    public class MoveSetBehaviourFactory : IFactory<MoveSetConfig, DiContainer, MoveSetBehaviour>
    {
        public MoveSetBehaviour Create(MoveSetConfig config, DiContainer container) =>
            new(new StateMachine(CreateTransitionStrategy(config, container)));

        private IStateTransitionStrategy CreateTransitionStrategy(MoveSetConfig config, DiContainer container)
        {
            var moves = CreateMoves(config.Moves, container).ToArray();
            CreateTransitions(moves);
            var strategy = new IdBasedTransitionStrategy
            {
                StartStateId = config.Moves.First().Id,
                TransitionMap = moves.ToDictionary(n => n.Id, n => (IIdBasedTransitionableState)n)
            };
            return strategy;
        }

        private IEnumerable<Move> CreateMoves(IEnumerable<MoveConfig> moveConfigs, DiContainer container) => 
            moveConfigs.Select(n => container.Instantiate(n.MoveType, new object[] {n})).Cast<Move>();

        private void CreateTransitions(Move[] moves)
        {
            foreach (var move in moves)
            {
                var transitions = moves.Where(n => move.Config.Transitions.Contains(n.Id)).ToArray();
                Assert.IsTrue(transitions.Length > 0);
                move.Transitions = transitions;
            }
        }
    }
}
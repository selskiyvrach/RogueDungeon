using System;
using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using UnityEngine.Assertions;
using Zenject;

namespace Common.MoveSets
{
    public class MoveSetFactory : IFactory<MoveSetConfig, MoveSetBehaviour>
    {
        private readonly DiContainer _container;

        public MoveSetFactory(DiContainer container) => 
            _container = container;

        public MoveSetBehaviour Create(MoveSetConfig config) =>
            new(CreateStateMachine(config));
        
        public T Create<T>(MoveSetConfig config) where T : MoveSetBehaviour => 
            (T)Activator.CreateInstance(typeof(T), CreateStateMachine(config));

        private StateMachine CreateStateMachine(MoveSetConfig config) => 
            new(CreateTransitionStrategy(config), config.DebugName);

        private IStateTransitionStrategy CreateTransitionStrategy(MoveSetConfig config)
        {
            var moves = CreateMoves(config.Moves.Concat(config.ExtendsConfigs.SelectMany(n => n.Moves))).ToArray();
            CreateTransitions(moves);
            var strategy = new IdBasedTransitionStrategy
            {
                StartStateId = config.FirstMove.Id,
                TransitionMap = moves.ToDictionary(n => n.Id, n => (IIdBasedTransitionableState)n)
            };
            return strategy;
        }

        private IEnumerable<Move> CreateMoves(IEnumerable<MoveConfig> moveConfigs) => 
            moveConfigs.Select(n => _container.Instantiate(n.MoveType, new object[] {n})).Cast<Move>();

        private void CreateTransitions(Move[] moves)
        {
            foreach (var move in moves)
            {
                var transitions = move.Config.Transitions.Select(n => moves.First(m => m.Id == n)).ToArray();
                Assert.IsTrue(transitions.Length > 0);
                move.Transitions = transitions;
            }
        }
    }
}
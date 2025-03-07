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
            new(CreateMoves(config.Moves), config.FirstMove.Id);
        
        public TBehaviour Create<TBehaviour, TMove>(MoveSetConfig config) where TBehaviour : MoveSetBehaviour where TMove : Move => 
            (TBehaviour)Activator.CreateInstance(typeof(TBehaviour), CreateMoves(config.Moves).Cast<TMove>(), config.FirstMove.Id);

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
            {
                try
                {
                    var transitions = move.Config.Transitions.Select(n => new Transition(moves.First(m => m.Id == n.MoveId), n.CanInterrupt)).ToArray();
                    Assert.IsTrue(transitions.Length > 0);
                    move.Transitions = transitions;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
        }
    }
}
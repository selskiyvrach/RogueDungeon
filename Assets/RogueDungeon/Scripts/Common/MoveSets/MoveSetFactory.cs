using System.Linq;
using Zenject;

namespace Common.MoveSets
{
    public class MoveSetFactory : IFactory<MoveSetConfig, MoveSet>
    {
        public MoveSet Create(MoveSetConfig config)
        {
            var idleMove = new Move("idle", () => config.IdleAnimation, () => config.IdleAnimation.length / config.IdleAnimationSpeed);
            var moves = config.MoveConfigs.Select(n => new Move(n.Name, () => n.Animation, () => n.Duration)).ToList();
            foreach (var move in moves)
            {
                var moveConfig = config.MoveConfigs.First(m => m.Name == move.Name);
                move.TransitionsUnderlyingList.AddRange(moves.Where(n => moveConfig.Transitions.Contains(n.Name)));
            }
            return new MoveSet(moves, idleMove);
        }
    }
}
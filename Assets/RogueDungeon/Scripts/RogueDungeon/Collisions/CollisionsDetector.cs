using System.Collections.Generic;
using System.Linq;
using Common.Registries;
using Common.UtilsDotNet;
using RogueDungeon.Entities;

namespace RogueDungeon.Collisions
{
    public class CollisionsDetector : ICollisionsDetector
    {
        private readonly IRegistry<IGameEntity> _gameEntities;

        public CollisionsDetector(IRegistry<IGameEntity> gameEntities) =>
            _gameEntities = gameEntities;

        public IEnumerable<Collision> GetCollisions(Positions positionsMask) => 
            _gameEntities.GetAll<ICollidable>(n => (n.Position & positionsMask) != 0).Select(n => new Collision(n, n.Position));
    }
}
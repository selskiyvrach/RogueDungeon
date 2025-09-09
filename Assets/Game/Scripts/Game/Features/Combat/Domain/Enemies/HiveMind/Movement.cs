using Game.Libs.Combat;
using UnityEngine;

namespace Game.Features.Combat.Domain.Enemies.HiveMind
{
    public struct Movement
    {
        public Enemy Target;
        public EnemyPosition DestinationPosition;
        public Vector2 StartCoordinates;
        public Vector2 DestinationCoordinates;
    }
}
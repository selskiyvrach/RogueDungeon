using UnityEngine;

namespace RogueDungeon.Enemies.HiveMind
{
    public struct Movement
    {
        public Enemy Target;
        public EnemyPosition DestinationPosition;
        public Vector2 StartCoordinates;
        public Vector2 DestinationCoordinates;
    }
}
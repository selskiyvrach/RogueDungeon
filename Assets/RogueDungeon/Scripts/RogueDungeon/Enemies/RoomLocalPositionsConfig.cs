using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class RoomLocalPositionsConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 MiddleEnemyPos {get; private set;}
        [field: SerializeField] public Vector2 LeftEnemyPos {get; private set;}
        [field: SerializeField] public Vector2 RightEnemyPos {get; private set;}
    }
}
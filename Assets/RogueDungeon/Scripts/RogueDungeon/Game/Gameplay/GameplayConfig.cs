using RogueDungeon.Enemies;
using UnityEngine;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyConfig TestEnemy { get; private set; }
    }
}
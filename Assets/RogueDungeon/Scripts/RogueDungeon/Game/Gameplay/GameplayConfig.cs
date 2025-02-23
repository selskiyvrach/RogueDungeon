using RogueDungeon.Enemies;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyConfig TestEnemy { get; private set; }
        [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
    }
}
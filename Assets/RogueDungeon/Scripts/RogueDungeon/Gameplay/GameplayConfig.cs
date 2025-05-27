using Enemies;
using Input;
using Levels;
using UnityEngine;

namespace Gameplay
{
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyConfig TestEnemy { get; private set; }
        [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
        [field: SerializeField] public InputFilter ExplorationInputFilter { get; set; }
        [field: SerializeField] public InputFilter CombatInputFilter { get; set; }
    }
}
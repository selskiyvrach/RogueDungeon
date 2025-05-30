using Game.Features.Levels.Domain;
using Game.Libs.Input;
using UnityEngine;

namespace Game.Features.Gameplay.Domain
{
    public class GameplayConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyConfig TestEnemy { get; private set; }
        [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
        [field: SerializeField] public InputFilter ExplorationInputFilter { get; set; }
        [field: SerializeField] public InputFilter CombatInputFilter { get; set; }
    }
}
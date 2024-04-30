using JetBrains.Annotations;
using RogueDungeon.Stats;

namespace RogueDungeon.Characters
{
    public class CombatState
    {
        public Positions positions { get; set; }
        public DodgeState DodgeState { get; set; }
        public bool BlockIsRaised { get; set; }
        public ISurroundingCharactersProvider SurroundingCharacters { get; set; }
        public ISurroundingsProvider Surroundings { get; set; }
        [CanBeNull] public IStatsProvider BlockingWeaponStats { get; set; }
    }
}
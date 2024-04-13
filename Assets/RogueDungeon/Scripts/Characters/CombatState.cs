using JetBrains.Annotations;
using RogueDungeon.Stats;

namespace RogueDungeon.Characters
{
    public class CombatState
    {
        public DodgeState DodgeState { get; set; }
        public bool BlockIsRaised { get; set; }
        [CanBeNull] public IStatsProvider BlockingWeaponStats { get; set; }
        [CanBeNull] public Character Enemy { get; set; }
    }
}
using RogueDungeon.Combat;
using RogueDungeon.Player;

namespace RogueDungeon.Game.Gameplay
{
    public class Gameplay : Common.Behaviours.Behaviour
    {
        private readonly ICombatantsRegistry _combatantsRegistry;
        private readonly IPlayerSpawner _playerSpawner;

        public Gameplay(IPlayerSpawner playerSpawner, ICombatantsRegistry combatantsRegistry)
        {
            _playerSpawner = playerSpawner;
            _combatantsRegistry = combatantsRegistry;
        }

        public override void Enable()
        {
            base.Enable();
            _playerSpawner.Spawn();
        }
    }
}
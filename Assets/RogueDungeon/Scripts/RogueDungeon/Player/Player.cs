using RogueDungeon.Behaviours.WeaponWielding;

namespace RogueDungeon.Player
{
    public class Player
    {
        private readonly Behaviour _weaponBehaviour;

        public Player(Behaviour weaponBehaviour)
        {
            _weaponBehaviour = weaponBehaviour;
            _weaponBehaviour.Enable();
        }
    }
}
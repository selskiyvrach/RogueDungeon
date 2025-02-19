using RogueDungeon.Combat;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;

namespace RogueDungeon.Player
{
    public class Player : IDodger, IPlayerCombatant 
    {
        private readonly PlayerHands _playerHands;
        public PlayerDodgeState DodgeState { get; set; }

        public Player(PlayerHands playerHands)
        {
            _playerHands = playerHands;
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
    
    // create enemy class
    // installer
    // factory
    // spawner
    // install factory
    // create an enemy in gameplay class
}
using RogueDungeon.Combat;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;
using RogueDungeon.Scripts.RogueDungeon.Combat;

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
}
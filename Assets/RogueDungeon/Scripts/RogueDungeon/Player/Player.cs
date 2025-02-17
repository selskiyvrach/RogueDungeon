using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;

namespace RogueDungeon.Player
{
    public class Player : IDodger 
    {
        private readonly PlayerHands _playerHands;
        public DodgeState DodgeState { get; set; }

        public Player(PlayerHands playerHands)
        {
            _playerHands = playerHands;
        }
    }
}
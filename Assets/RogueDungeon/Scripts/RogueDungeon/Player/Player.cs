using RogueDungeon.Player.Behaviours.Dodge;

namespace RogueDungeon.Player
{
    public class Player : IDodger 
    {
        public DodgeState DodgeState { get; set; }

        public Player()
        {
            
        }
    }
}
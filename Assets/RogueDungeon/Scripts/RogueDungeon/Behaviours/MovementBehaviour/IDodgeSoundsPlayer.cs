namespace RogueDungeon.Behaviours.MovementBehaviour
{
    public interface IDodgeSoundsPlayer
    {
        void PlayDodgeSound();
    }
    
    public class DummyDodgeSoundsPlayer : IDodgeSoundsPlayer
    {
        public void PlayDodgeSound() { }
    }
}
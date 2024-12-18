namespace RogueDungeon.Behaviours.MovementBehaviour
{
    public interface IMovementAnimationsPlayer
    {
        void PlayDodgeLeft(float duration);
        void PlayDodgeRight(float duration);
        void PlayIdle();
    }
}
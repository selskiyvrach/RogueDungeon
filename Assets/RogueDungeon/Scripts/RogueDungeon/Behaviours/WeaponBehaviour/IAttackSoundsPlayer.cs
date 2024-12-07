namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackSoundsPlayer
    {
        void PlayAttackPrepareSound();
        void PlayAttackExecuteSound();
        void PlayAttackFinishSound();
    }
    
    public class DummyAttackSoundsPlayer : IAttackSoundsPlayer
    {
        public void PlayAttackPrepareSound() { }

        public void PlayAttackExecuteSound() { }

        public void PlayAttackFinishSound() { }
    }
}
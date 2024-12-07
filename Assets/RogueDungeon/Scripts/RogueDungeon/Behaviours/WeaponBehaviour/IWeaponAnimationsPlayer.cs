using System;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IWeaponAnimationsPlayer
    {
        event Action OnHitKeyframe;
        void PlayAttackPrepare(float duration);
        void PlayAttackExecute(float duration);
        void PlayAttackFinish(float duration);
        void PlayIdle();
    }
    
    public class DummyWeaponAnimationsPlayer : IWeaponAnimationsPlayer
    {
        public event Action OnHitKeyframe;
        public void PlayAttackPrepare(float duration) { }

        public void PlayAttackExecute(float duration) { }

        public void PlayAttackFinish(float duration) { }
        public void PlayIdle()
        {
        }
    }
}
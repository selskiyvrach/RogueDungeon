using System;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackInputProvider
    {
        bool HasAttackInput();
    }
    
    public class DummyAttackInputProvider : IAttackInputProvider
    {
        private readonly Func<bool> _hasAttackInput;

        public DummyAttackInputProvider(Func<bool> hasAttackInput) => 
            _hasAttackInput = hasAttackInput;

        public bool HasAttackInput() => 
            _hasAttackInput.Invoke(); 
    }
    
}
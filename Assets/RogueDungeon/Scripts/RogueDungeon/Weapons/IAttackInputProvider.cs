using System;

namespace RogueDungeon.Weapons
{
    public interface IAttackInputProvider
    {
        bool HasAttackInput();
    }
    
    public class DummyWeaponInputProvider : IAttackInputProvider
    {
        private readonly Func<bool> _hasAttackInput;

        public DummyWeaponInputProvider(Func<bool> hasAttackInput) => 
            _hasAttackInput = hasAttackInput;

        public bool HasAttackInput() => 
            _hasAttackInput.Invoke(); 
    }
}
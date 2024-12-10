using System;

namespace RogueDungeon.Weapons
{
    public interface IWeaponInputProvider
    {
        bool HasAttackInput();
    }
    
    public class DummyWeaponInputProvider : IWeaponInputProvider
    {
        private readonly Func<bool> _hasAttackInput;

        public DummyWeaponInputProvider(Func<bool> hasAttackInput) => 
            _hasAttackInput = hasAttackInput;

        public bool HasAttackInput() => 
            _hasAttackInput.Invoke(); 
    }
}
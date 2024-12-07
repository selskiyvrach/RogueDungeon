using System;

namespace RogueDungeon.Behaviours.MovementBehaviour
{
    public interface IDodgeInputProvider
    {
        bool HasDodgeRightInput();
        bool HasDodgeLeftInput();
    }
    
    public class DummyDodgeInputProvider : IDodgeInputProvider
    {
        private readonly Func<bool> _hasRightDodge;
        private readonly Func<bool> _hasLeftDodge;

        public DummyDodgeInputProvider(Func<bool> hasRightDodge, Func<bool> hasLeftDodge)
        {
            _hasRightDodge = hasRightDodge;
            _hasLeftDodge = hasLeftDodge;
        }

        public bool HasDodgeRightInput() => 
            _hasRightDodge.Invoke();

        public bool HasDodgeLeftInput() => 
            _hasLeftDodge.Invoke();
    }
}
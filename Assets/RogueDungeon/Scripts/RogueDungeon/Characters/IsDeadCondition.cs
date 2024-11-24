using Common.FSM;

namespace RogueDungeon.Characters
{
    public class IsDeadCondition : ICondition
    {
        private readonly IDieable _dieable;

        public IsDeadCondition(IDieable dieable) => 
            _dieable = dieable;

        public bool IsMet() => 
            _dieable.IsDead;
    }
}
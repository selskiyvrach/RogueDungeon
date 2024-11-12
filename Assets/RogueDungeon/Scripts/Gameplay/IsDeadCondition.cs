using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay
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
namespace RogueDungeon.Weapons
{
    public interface IAttackMediator
    {
        bool CanStartAttack();
        bool IsAttackInterruptable { set; }
    }

    public class DummyAttackMediator : IAttackMediator
    {
        public bool CanStartAttack() => true;
        public bool IsAttackInterruptable { get; set; }
    }
}
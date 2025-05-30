namespace Game.Features.Combat.Domain
{
    public interface IWeaponOfAttack
    {
        float Damage { get; }
        float PoiseDamage { get; }
    }
}
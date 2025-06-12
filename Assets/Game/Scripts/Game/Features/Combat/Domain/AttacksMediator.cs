namespace Game.Features.Combat.Domain
{
    public class AttacksMediator
    {
        // private readonly IEnemiesRegistry _enemiesRegistry;
        // private readonly IPlayerRegistry _playerRegistry;
        //
        // public AttacksMediator(IEnemiesRegistry enemiesRegistry, IPlayerRegistry playerRegistry)
        // {
        //     _enemiesRegistry = enemiesRegistry;
        //     _playerRegistry = playerRegistry;
        // }
        
        public void MediatePlayerAttack()
        {
            // if (_enemiesRegistry.Enemies.FirstOrDefault(n => n.Position == EnemyPosition.Middle) is not {} enemy)
            // {
            //     // miss
            //     return;
            // }
            //
            // var modifier = _playerRegistry.Player.IsDoubleGrip ? _playerRegistry.Player.DoubleGripDamageBonus : 1f;
            // enemy.TakeDamage(weapon.Damage * modifier, weapon.PoiseDamage * modifier);
        }

        public void MediateEnemyAttack(float damage, EnemyAttackDirection attackDirection)
        {
            // attackDirection.ThrowIfNone();
            // if(_playerRegistry.Player is not {} player)
            //     return;
            //
            // if (attackDirection == EnemyAttackDirection.Left && player.IsDodgingRight || attackDirection == EnemyAttackDirection.Right && player.IsDodgingLeft)
            // {
            //     player.OnDodged();
            //     return;
            // }
            //
            // if (player is { IsBlocking: true} blocker)
            // {
            //     var staminaCost = damage * blocker.BlockStaminaCostMultiplier;
            //     damage = Mathf.Max(0, staminaCost - player.Stamina.Current) / blocker.BlockStaminaCostMultiplier; 
            //     player.Stamina.AddDelta(- staminaCost);
            //     
            //     blocker.OnBlocked();
            // }
            //
            // player.Health.AddDelta(- damage);
        }
    }
}
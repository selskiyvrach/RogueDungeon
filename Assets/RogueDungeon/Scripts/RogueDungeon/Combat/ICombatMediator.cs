using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
using RogueDungeon.Scripts.RogueDungeon.Combat;

namespace RogueDungeon.Combat
{
    public interface IPlayerRegistry
    {
        void RegisterPlayer(IPlayerCombatant player);
        void UnregisterPlayer(IPlayerCombatant player);
    }

    public interface IEnemiesRegistry
    {
        void RegisterEnemy(IEnemyCombatant enemy);
        void UnregisterEnemy(IEnemyCombatant enemy);
    }

    public interface IAttacksMediator
    {
        void MediatePlayerAttack(IPlayerAttackInfo attackInfo);
        void MediateEnemyAttack(IEnemyAttackInfo attackInfo);
    }

    public class CombatantsRegistry : IPlayerRegistry, IEnemiesRegistry
    {
        public IPlayerCombatant Player { get; private set; }
        public List<IEnemyCombatant> Enemies { get; private set; } = new(3);

        public void RegisterPlayer(IPlayerCombatant player) => 
            Player = player;

        public void UnregisterPlayer(IPlayerCombatant player)
        {
            if(player == Player)
                Player = null;
            else
                throw new System.ArgumentException("Player is not registered");
        }

        public void RegisterEnemy(IEnemyCombatant enemy) => 
            Enemies.Add(enemy);

        public void UnregisterEnemy(IEnemyCombatant enemy)
        {
            if(!Enemies.Remove(enemy))
                throw new System.ArgumentException("Enemy is not registered");
        }
    }

    public class AttacksMediator : IAttacksMediator
    {
        private readonly CombatantsRegistry _registry;
        
        public void MediatePlayerAttack(IPlayerAttackInfo attackInfo)
        {
            if (_registry.Enemies.FirstOrDefault(n => n.Position == EnemyPosition.Middle) is not {} enemy)
            {
                // miss
                return;
            }

            enemy.TakeDamage(attackInfo.Damage);
        }

        public void MediateEnemyAttack(IEnemyAttackInfo attackInfo)
        {
            attackInfo.AttackDirection.ThrowIfNone();
            if(_registry.Player is not {} player)
                return;

            if (attackInfo.AttackDirection == EnemyAttackDirection.Left &&
                player.DodgeState == PlayerDodgeState.DodgingRight
                || attackInfo.AttackDirection == EnemyAttackDirection.Right &&
                player.DodgeState == PlayerDodgeState.DodgingLeft)
            {
                // miss
                return;
            }
            
            player.TakeDamage(attackInfo.Damage);
        }
    }

    public interface IPlayerCombatant : ICombatTarget
    {
        PlayerDodgeState DodgeState { get; }
    }

    public interface IEnemyCombatant : ICombatTarget
    {
        EnemyPosition Position { get; }
    }

    public interface ICombatTarget
    {
        void TakeDamage(float damage);
    }

    public interface IPlayerAttackInfo : IDamageInfo
    {
     
    }

    public interface IEnemyAttackInfo : IDamageInfo
    {
        EnemyAttackDirection AttackDirection { get; }
    }

    public interface IDamageInfo
    {
        float Damage { get; }   
    }
}
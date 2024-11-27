using System.Collections.Generic;
using System.Linq;
using Common.DotNetUtils;
using Common.Registries;
using RogueDungeon.Animations;
using RogueDungeon.Enemies;
using RogueDungeon.Entities;
using RogueDungeon.Entities.Prameters;
using RogueDungeon.Entities.Properties;
using RogueDungeon.Weapons;

namespace RogueDungeon.Characters
{
    public interface ITargetsMask
    {
        IEnumerable<IGameEntity> GetTargets(IRegistry<IGameEntity> entities);
    }

    public abstract class TargetsMask : ITargetsMask
    {
        public IEnumerable<IGameEntity> GetTargets(IRegistry<IGameEntity> entities) =>
            entities.GetAll<IGameEntity>(IsValidTarget);

        protected abstract bool IsValidTarget(IGameEntity entity);
    }
    
    public class EnemyTargetsMask : TargetsMask
    {
        protected override bool IsValidTarget(IGameEntity entity) => 
            entity is Player.Player;
    }

    public class PlayerTargetsMask : TargetsMask
    {
        protected override bool IsValidTarget(IGameEntity entity) => 
            entity is IEnemy;
    }

    public interface IEnemy : IGameEntity
    {
    }

    public readonly struct AttackEvent : IAnimationEvent
    {
    }
}
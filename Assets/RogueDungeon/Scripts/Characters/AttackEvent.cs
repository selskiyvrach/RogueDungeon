using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Animations;
using RogueDungeon.Enemies;
using RogueDungeon.Entities;
using RogueDungeon.Entities.Prameters;
using RogueDungeon.Entities.Properties;
using RogueDungeon.Services.Extensions;
using RogueDungeon.Services.Registries;

namespace RogueDungeon.Characters
{
    public interface ITargetsMask
    {
        IEnumerable<IAttackTarget> GetTargets(IRegistry<IRootEntity> entities);
    }

    public abstract class TargetsMask : ITargetsMask
    {
        public IEnumerable<IAttackTarget> GetTargets(IRegistry<IRootEntity> entities) =>
            entities.GetAll<IAttackTarget>(IsValidTarget);

        protected abstract bool IsValidTarget(IRootEntity entity);
    }
    
    public class EnemyTargetsMask : TargetsMask
    {
        protected override bool IsValidTarget(IRootEntity entity) => 
            entity is Entities.Player;
    }

    public class PlayerTargetsMask : TargetsMask
    {
        protected override bool IsValidTarget(IRootEntity entity) => 
            entity.Properties.Any(n => n is Property<EnemyPosition> pos && pos.Value == EnemyPosition.Middle);
    }
    
    public interface IAttackData
    {
        ITargetsMask TargetsMask { get; }
        IRegistry<Parameter> Parameters { get; }
        IRegistry<Property> Properties { get; }
    }

    public readonly struct AttackEvent : IAnimationEvent
    {
        public readonly IAttackData AtaAttackData;
        public AttackEvent(IAttackData ataAttackData) => 
            AtaAttackData = ataAttackData;
    }
}
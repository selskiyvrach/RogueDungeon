using System;
using System.Linq;
using RogueDungeon.Characters;
using RogueDungeon.Entities.Prameters;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.Extensions;
using RogueDungeon.Services.Registries;

namespace RogueDungeon.Entities
{
    public class AttackHandler : IEventHandler<AttackEvent>
    {
        private readonly IRegistry<IRootEntity> _entities;

        public AttackHandler(IRegistry<IRootEntity> entities) => 
            _entities = entities;

        public void Handle(AttackEvent @event)
        {
            var target = @event.AtaAttackData.TargetsMask.GetTargets(_entities).FirstOrDefault();
            if(target == null)
                return;
            
            var totalFlat = 0f;
            var totalFactor = 1f;
            
            foreach (var value in @event.AtaAttackData.Parameters.GetAll<AttackDamage>())
            {
                switch (value.ParamType)
                {
                    case Parameter.Type.Flat: totalFlat += value.Value;
                        break;
                    case Parameter.Type.Percent: totalFactor *= 1 + value.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            target.TakeDamage(totalFlat * totalFactor);
        }
    }
}
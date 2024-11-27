using Common.Events;
using Common.Registries;
using RogueDungeon.Characters;

namespace RogueDungeon.Entities
{
    public class AttackHandler : IEventHandler<AttackEvent>
    {
        private readonly IRegistry<IGameEntity> _entities;

        public AttackHandler(IRegistry<IGameEntity> entities) => 
            _entities = entities;

        public void HandleEvent(AttackEvent @event)
        {
            // var target = @event.Weapon.TargetsMask.GetTargets(_entities).FirstOrDefault();
            // if(target == null)
            //     return;
            //
            // var totalFlat = 0f;
            // var totalFactor = 1f;
            //
            // foreach (var value in @event.Weapon.Parameters.GetAll<AttackDamage>())
            // {
            //     switch (value.ParamType)
            //     {
            //         case Parameter.Type.Flat: totalFlat += value.Value;
            //             break;
            //         case Parameter.Type.Percent: totalFactor *= 1 + value.Value;
            //             break;
            //         default:
            //             throw new ArgumentOutOfRangeException();
            //     }
            // }
            //
            // target.TakeDamage(totalFlat * totalFactor);
        }
    }
}
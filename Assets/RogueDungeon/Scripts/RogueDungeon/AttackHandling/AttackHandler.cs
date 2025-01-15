using System.Collections.Generic;
using Common.Behaviours;

namespace RogueDungeon.Scripts.RogueDungeon.AttackHandling
{
    public interface IAttackTargetsSelector
    {
        IEnumerable<IAttackTarget> GetFromPossibleTargets(IEnumerable<IAttackTarget> possibleTargets);
    }

    public interface IAttackTarget
    {
        
    }

    public interface IPlayerTarget : IAttackTarget
    {
    }

    public interface IEnemyTarget : IAttackTarget
    {
    }

    public interface IAttackingSource
    {
        IAttackTargetsSelector TargetsSelector { get; }
        
    }

    public interface IAllPossibleTargetsGetter
    {
        IEnumerable<IAttackTarget> Get();
    }
    
    // weapon attack handler
        // wielder
        // weapon info
        // combo info
        // combo index getter
        // targets scanner
        
    // weapon wielder
        // weapon attack handler
            // handle attack
            
    // extract abstractions later when they're obvious

    public class AttackHandler : Behaviour
    {
        private readonly IAttackHitEventObservable _hitEventObservable;
        private readonly IAllPossibleTargetsGetter _possibleTargetsGetter;
        private readonly IAttackingSource _sourceGetter;

        public AttackHandler(IAttackHitEventObservable hitEventObservable, IAttackingSource sourceGetter, IAllPossibleTargetsGetter possibleTargetsGetter)
        {
            _hitEventObservable = hitEventObservable;
            _sourceGetter = sourceGetter;
            _possibleTargetsGetter = possibleTargetsGetter;
        }

        public override void Enable()
        {
            base.Enable();
            _hitEventObservable.OnHit += HandleHit;
        }

        public override void Disable()
        {
            base.Disable();
            _hitEventObservable.OnHit -= HandleHit;
        }

        private void HandleHit(IAttackingSource attackingSource)
        {
            
        }
    }
}
using Common.Fsm;
using Common.Parameters;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public class WeaponWielderBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private WeaponTimings _timings;
        
        public override void InstallBindings()
        {
            Container.NewSingleParameter<IIdleAnimationSpeed>(() => _timings.IdleAnimationSpeed);
            Container.NewSingleParameter<IAttackExecutionDuration>(() => _timings.AttackExecutionDuration);
            Container.NewSingleParameter<IAttackAttackTransitionDuration>(() => _timings.AttackAttackTransitionDuration);
            Container.NewSingleParameter<IAttackIdleTransitionDuration>(() => _timings.AttackIdleTransitionDuration);
            
            Container.NewSingleInterfaces<WeaponWielderBehaviourContext>();
            Container.NewSingle<WeaponWielderBehaviour>().WithArguments(new StatesFactoryWithCache(Container));
            Container.NewSingleNonLazy<WeaponWielderBehaviourEnabler>();
        }
    }
}
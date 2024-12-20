using Common.Fsm;
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
            Container.InstanceSingle<IIdleAnimationSpeed>(new IdleAnimationSpeed(() => _timings.IdleAnimationSpeed));
            Container.InstanceSingle<IAttackExecutionDuration>(new AttackExecutionDuration(() => _timings.AttackExecutionDuration));
            Container.InstanceSingle<IAttackAttackTransitionDuration>(new AttackAttackTransitionDuration(() => _timings.AttackAttackTransitionDuration));
            Container.InstanceSingle<IAttackIdleTransitionDuration>(new AttackIdleTransitionDuration(() => _timings.AttackIdleTransitionDuration));
            Container.NewSingleInterfaces<WeaponWielderBehaviourContext>();
            Container.NewSingle<WeaponWielderBehaviour>().WithArguments(new StatesFactoryWithCache(Container));
            Container.NewSingleNonLazy<WeaponWielderBehaviourEnabler>();
        }
    }
}
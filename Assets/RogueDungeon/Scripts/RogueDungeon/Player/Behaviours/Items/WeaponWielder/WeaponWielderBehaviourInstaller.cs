using Common.Animations;
using Common.Behaviours;
using Common.Parameters;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponWielderBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private WeaponTimings _timings;
        [SerializeField] private EquipmentAnimationsPlayer _animationPlayer;
        
        public override void InstallBindings()
        {
            var subContainer = Container.BehaviourSubcontainer<WeaponWielderBehaviour, WeaponWielderInternalFacade, WeaponWielderExternalFacade>();
            subContainer.NewSingleParameter<IIdleAnimationSpeed>(() => _timings.IdleAnimationSpeed);
            subContainer.NewSingleParameter<IAttackExecutionDuration>(() => _timings.AttackExecutionDuration);
            subContainer.NewSingleParameter<IAttackAttackTransitionDuration>(() => _timings.AttackAttackTransitionDuration);
            subContainer.NewSingleParameter<IAttackIdleTransitionDuration>(() => _timings.AttackIdleTransitionDuration);
            subContainer.InstanceSingleInterfaces(_animationPlayer);
            
            subContainer.NewSingleAutoResolve<WeaponWielderBehaviourEnabler>();
        }
    }
}
using Common.Fsm;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class WeaponBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private float _idleAnimationSpeed = 1;
        [SerializeField] private float _attackExecutionDuration = .3f;
        [SerializeField] private float _attackAttackTransitionDuration = .4f;
        [SerializeField] private float _attackIdleTransitionDuration = .4f;

        public override void InstallBindings()
        {
            Container.InstanceSingle<IIdleAnimationSpeed>(new IdleAnimationSpeed(_idleAnimationSpeed));
            Container.InstanceSingle<IAttackExecutionDuration>(new AttackExecutionDuration(_attackExecutionDuration));
            Container.InstanceSingle<IAttackAttackTransitionDuration>(new AttackAttackTransitionDuration(_attackAttackTransitionDuration));
            Container.InstanceSingle<IAttackIdleTransitionDuration>(new AttackIdleTransitionDuration(_attackIdleTransitionDuration));
            Container.NewSingleInterfaces<WeaponBehaviourContext>();
            Container.NewSingle<WeaponBehaviour>().WithArguments(new StatesFactoryWithCache(Container));
            Container.NewSingleNonLazy<WeaponBehaviourEnabler>();
        }
    }
}
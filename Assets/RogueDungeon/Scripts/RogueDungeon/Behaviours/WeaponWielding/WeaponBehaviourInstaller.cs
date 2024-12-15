using Common.Animations;
using Common.Fsm;
using Common.ZenjectUtils;
using RogueDungeon.Animations;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class WeaponBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private AnimationPlayer _animator;

        public override void InstallBindings()
        {
             Container.InstanceSingle<IAnimator>(_animator);
             Container.InstanceSingle<IStatesFactory>(new PrecachedFactory(Container, new []
             {
                 typeof(IdleState), 
                 typeof(AttackExecutionState),
                 typeof(AttackFinishState),
                 typeof(AttackPrepareState),
                 typeof(AttackToAttackTransitionState),
             }));
             Container.NewSingleInterfacesAndSelf<Behaviour>();
        }
    }
}
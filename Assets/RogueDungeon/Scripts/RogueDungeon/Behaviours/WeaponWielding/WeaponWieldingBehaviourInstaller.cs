using Common.Animations;
using Common.Fsm;
using Common.ZenjectUtils;
using RogueDungeon.Animations;
using RogueDungeon.PlayerInput;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class WeaponWieldingBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private TestEnvironment _testEnvironment;
        [SerializeField] private AnimationPlayer _animator;
        private Behaviour _behaviour;

        public override void InstallBindings()
        {
             Container.InstanceSingle<IDurations>(_testEnvironment);
             Container.InstanceSingle<IComboInfo>(_testEnvironment);
             Container.InstanceSingle<IComboCounter>(_testEnvironment);
             Container.InstanceSingle<IControlState>(_testEnvironment);
             Container.InstanceSingle<IAnimator>(_animator);
             
             Container.NewSingle<IInput, CharacterInput>();
             
             Container.NewSingle<IStatesFactory, StatesFactoryWithCache>();
            
             _behaviour = Container.NewSingleResolve<Behaviour>();
        }

        public override void Start()
        {
            base.Start();
            _behaviour.Enable();
        }
    }
}
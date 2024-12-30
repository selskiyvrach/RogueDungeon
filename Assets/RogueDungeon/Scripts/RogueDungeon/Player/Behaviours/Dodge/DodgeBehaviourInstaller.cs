using Common.Behaviours;
using Common.Fsm;
using Common.Parameters;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private DodgeParameters _parameters;
        
        public override void InstallBindings()
        {
            var subContainer = Container.CreateSubContainer();
            subContainer.NewSingleParameter<IDodgeDuration>(() => _parameters.Duration);
            subContainer.NewSingle<DodgeBehaviour>().WithArguments(new StatesFactoryWithCache(Container)).NonLazy();
            subContainer.NewSingle<BehaviourAutorunner<DodgeBehaviour>>();
        }
    }
}
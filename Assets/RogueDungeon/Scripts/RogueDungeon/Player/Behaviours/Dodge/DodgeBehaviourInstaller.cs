using Common.Behaviours;
using Common.Parameters;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private DodgeParameters _parameters;
        
        public override void InstallBindings()
        {
            var subContainer = Container.BehaviourSubcontainer<DodgeBehaviour, DodgeInternalFacade, DodgeExternalFacade>(autoRunBehaviour: true);
            subContainer.NewSingleParameter<IDodgeDuration>(() => _parameters.Duration);
        }
    }
}
using System;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private DodgeParameters _parameters;
        
        public override void InstallBindings()
        {
            throw new NotImplementedException();
            // var subContainer = Container.BehaviourSubcontainer<DodgeBehaviour, DodgeInternalFacade, DodgeExternalFacade>(autoRunBehaviour: true);
            // subContainer.NewSingleParameter<IDodgeDuration>(() => _parameters.Duration);
        }
    }
}
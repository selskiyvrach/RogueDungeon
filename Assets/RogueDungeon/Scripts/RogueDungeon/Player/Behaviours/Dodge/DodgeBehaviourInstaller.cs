using Common.Fsm;
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
            Container.InstanceSingle<IDodgeDuration>(new DodgeDuration(() => _parameters.Duration));
            Container.NewSingle<DodgeBehaviour>().WithArguments(new StatesFactoryWithCache(Container));
        }
    }
}
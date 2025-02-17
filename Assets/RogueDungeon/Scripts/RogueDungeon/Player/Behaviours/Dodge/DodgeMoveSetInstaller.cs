using Common.Behaviours;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeMoveSetInstaller : MonoInstaller
    {
        [SerializeField] private MoveSetConfig _dodgeMoveSetConfig;
        
        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.InstanceSingle(new MoveSetBehaviourFactory().Create(_dodgeMoveSetConfig, container));
            container.AutoResolve<MoveSetBehaviour>();
            container.NewSingleAutoResolve<BehaviourAutorunner<MoveSetBehaviour>>();
        }
    }
}
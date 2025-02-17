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
            Container.InstanceSingle(new MoveSetBehaviourFactory().Create(_dodgeMoveSetConfig, Container));
            Container.AutoResolve<MoveSetBehaviour>();
            
            Container.NewSingle<BehaviourAutorunner<MoveSetBehaviour>>();
            Container.AutoResolve<BehaviourAutorunner<MoveSetBehaviour>>();
        }
    }
}
using Common.Behaviours;
using Common.UtilsZenject;
using RogueDungeon.Characters.Input;
using UnityEngine;
using Zenject;

namespace RogueDungeon.MoveSets
{
    public class DodgeMoveSetInstaller : MonoInstaller
    {
        [SerializeField] private MoveSetConfig _dodgeMoveSetConfig;
        
        public override void InstallBindings()
        {
            Container.NewSingle<ICharacterInput, PlayerInput>();
            Container.InstanceSingle(new MoveSetBehaviourFactory().Create(_dodgeMoveSetConfig, Container));
            Container.AutoResolve<MoveSetBehaviour>();
            
            Container.NewSingle<BehaviourAutorunner<MoveSetBehaviour>>();
            Container.AutoResolve<BehaviourAutorunner<MoveSetBehaviour>>();
        }
    }
}
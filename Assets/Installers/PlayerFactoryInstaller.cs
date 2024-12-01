using Common.GameObjectMarkers;
using Common.InstallerGenerator;
using Common.Properties;
using RogueDungeon.Player;
using RogueDungeon.PlayerInputCommands;
using UnityEngine;
using Zenject;

public class PlayerFactoryInstaller : ScriptableInstaller, IFactory<Player>
{
    [SerializeField] private PlayerGameObjectInstaller _gameObjectInstaller;
    
    private DiContainer _container;

    public override void Install(DiContainer container)
    {
        _container = container;
        container.InstanceSingle<IFactory<Player>, PlayerFactoryInstaller>(this);
    }

    public Player Create()
    {
        var subContainer = _container.CreateSubContainer();

        Instantiate(_gameObjectInstaller, subContainer.Resolve<PlayerParentObject>().transform).InstallToPlayerContext(subContainer);
        
        subContainer.NewSingleInterfacesAndSelf<Property<AttackState>>();
        subContainer.NewSingleInterfacesAndSelf<Property<DodgeState>>();
        subContainer.NewSingle<CharacterControlStateResolver>();
        subContainer.NewSingle<AttackBehaviour>();
        subContainer.NewSingle<DodgeBehaviour>();
        
        subContainer.NewSingle<ICharacterInput, CharacterInput>();
        
        return subContainer.NewSingleResolve<Player>();
    }
}

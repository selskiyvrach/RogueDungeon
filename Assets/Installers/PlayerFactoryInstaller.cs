using Common.InstallerGenerator;
using Common.Properties;
using RogueDungeon.Player;
using RogueDungeon.PlayerInputCommands;
using Zenject;

public class PlayerFactoryInstaller : ScriptableInstaller, IFactory<Player>
{
    public override void Install(DiContainer container) => 
        container.InstanceSingle<IFactory<Player>, PlayerFactoryInstaller>(this);

    public Player Create()
    {
        var subContainer = Container.CreateSubContainer();

        subContainer.NewSingle<IProperty<AttackState>, Property<AttackState>>();
        subContainer.NewSingle<IProperty<DodgeState>, Property<DodgeState>>();
        subContainer.NewSingle<CharacterControlStateResolver>();
        subContainer.NewSingle<AttackBehaviour>();
        subContainer.NewSingle<DodgeBehaviour>();
        
        subContainer.NewSingle<ICharacterInput, CharacterInput>();
        
        return subContainer.NewSingleResolve<Player>();
    }
}

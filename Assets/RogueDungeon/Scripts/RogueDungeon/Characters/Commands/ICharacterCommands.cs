namespace RogueDungeon.Characters.Commands
{
    public interface ICharacterCommands
    {
        bool TryConsume<T>() where T : ICharacterCommandDefinition;
    }
}
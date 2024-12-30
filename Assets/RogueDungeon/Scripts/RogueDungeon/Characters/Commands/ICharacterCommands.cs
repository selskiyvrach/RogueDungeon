namespace RogueDungeon.Characters.Commands
{
    public interface ICharacterCommands
    {
        bool TryConsume<T>() where T : ICharacterCommandDefinition;
        bool IsCurrentCommand<T>()where T : ICharacterCommandDefinition;
        void ConsumeCommand<T>()where T : ICharacterCommandDefinition;
    }
}
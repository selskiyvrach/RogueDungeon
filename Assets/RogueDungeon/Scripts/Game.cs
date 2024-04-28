using RogueDungeon.Characters;

namespace RogueDungeon
{
    public class Game
    {
        private readonly CharactersManager _charactersManager;

        public Game(CharacterFactory characterFactory) => 
            _charactersManager = new CharactersManager(characterFactory);

        public void CreateCharacter(string configName, Position position) => 
            _charactersManager.CreateCharacter(configName, position);

        public void Tick() => 
            _charactersManager.Tick();
    }
}
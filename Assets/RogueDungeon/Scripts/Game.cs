using RogueDungeon.Characters;

namespace RogueDungeon
{
    public class Game
    {
        private readonly CharactersManager _charactersManager;

        public Game(CharacterFactory characterFactory, CharacterScenePositions scenePositions) => 
            _charactersManager = new CharactersManager(characterFactory, scenePositions);

        public void CreateCharacter(string configName, Position position) => 
            _charactersManager.CreateCharacter(configName, position);

        public void Tick() => 
            _charactersManager.Tick();
    }
}
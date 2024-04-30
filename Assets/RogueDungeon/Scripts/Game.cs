using RogueDungeon.Characters;

namespace RogueDungeon
{
    public class Game
    {
        private readonly CharactersManager _charactersManager;

        public Game(CharacterFactory characterFactory, CharacterScenePositions scenePositions) => 
            _charactersManager = new CharactersManager(characterFactory, scenePositions);

        public void CreateCharacter(string configName, Positions positions) => 
            _charactersManager.CreateCharacter(configName, positions);

        public void Tick() => 
            _charactersManager.Tick();
    }
}
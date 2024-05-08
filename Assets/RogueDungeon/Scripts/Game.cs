using RogueDungeon.Characters;
using RogueDungeon.Input;

namespace RogueDungeon
{
    public class Game
    {
        private readonly CharactersManager _charactersManager;

        public Game(CharacterFactory characterFactory, CharacterScenePositions scenePositions)
        {
            _charactersManager = new CharactersManager(characterFactory, scenePositions);
            CreateCharacter("test-player", Position.Player);
            CreateCharacter("test-skeleton-swordsman", Position.Frontline);
            CreateCharacter("test-skeleton-swordsman", Position.BacklineLeft);
            CreateCharacter("test-skeleton-swordsman", Position.BacklineRight);
        }

        public void CreateCharacter(string configName, Position position)
        {
            _charactersManager.CreateCharacter(configName, position);
            Input.Input.SetModeState(Mode.Combat, true);
        }

        public void Tick()
        {
            _charactersManager.Tick();
        }
    }
}
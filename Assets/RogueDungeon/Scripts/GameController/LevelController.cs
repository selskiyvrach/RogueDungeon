using RogueDungeon.Characters;

namespace RogueDungeon.GameController
{
    public class LevelController
    {
        private readonly Character _player;
        private Character _enemy;

        public LevelController(CharacterConfig playerConfig)
        {
            _player = CharacterFactory.Create(playerConfig, null);
            
        }

        public void StartFight(CharacterConfig enemy)
        {
            _enemy = CharacterFactory.Create(enemy, null);
            
        }
    }
}
using System.Collections.Generic;
using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMind
    {
        private StateMachine _hiveMindBehaviour;
        private readonly IEnemiesRegistry _enemiesRegistry;

        public List<Enemy> Enemies => _enemiesRegistry.Enemies;
        public List<(Enemy enemy, EnemyPosition destination)> EnemiesToMove { get; } = new(3);
        public Queue<Enemy> AttackersQueue { get; } = new(3);
        public float SlackTime { get; set; }

        public HiveMind(IEnemiesRegistry enemiesRegistry) => 
            _enemiesRegistry = enemiesRegistry;

        public void SetBehaviour(StateMachine hiveMindBehaviour) => 
            _hiveMindBehaviour = hiveMindBehaviour;

        public void Tick(float deltaTime)
        {
            _hiveMindBehaviour.Tick(deltaTime);
            foreach (var enemy in Enemies) 
                enemy.Tick(deltaTime);
        }

        public void Initialize() => 
            _hiveMindBehaviour.Initialize();
    }
}
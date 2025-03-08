using System.Collections.Generic;
using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMind
    {
        private StateMachine _hiveMindBehaviour;
        public List<Enemy> Enemies { get; } = new(3);
        public List<(Enemy enemy, EnemyPosition destination)> EnemiesToMove { get; } = new(3);
        public Queue<Enemy> AttackersQueue { get; } = new(3);
        public float SlackTime { get; set; }

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
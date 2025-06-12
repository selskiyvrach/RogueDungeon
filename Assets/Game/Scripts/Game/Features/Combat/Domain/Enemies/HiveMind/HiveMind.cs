using System.Collections.Generic;
using System.Linq;
using Libs.Lifecycle;

namespace Game.Features.Combat.Domain.Enemies
{
    public class HiveMind : ITickable
    {
        private readonly HiveMindCombo _combo;
        public List<Enemy> Enemies { get; } = new();

        public HiveMind() => 
            _combo = new HiveMindCombo(this);

        public void Add(Enemy enemy)
        {
            Enemies.Add(enemy);
            enemy.Initialize();
        }

        public void Tick(float deltaTime)
        {
            PruneDeadAndTickAlive(deltaTime);
            if(!_combo.IsRunning)
                FillMiddlePositionIfEmpty();
            _combo.Tick(deltaTime);
        }

        private void FillMiddlePositionIfEmpty()
        {
            if(Enemies.All(n => n.OccupiedPosition != EnemyPosition.Middle) && Enemies.FirstOrDefault(n => n.IsIdle) is {} enemy)
                enemy.ChangePosition(EnemyPosition.Middle);
        }

        private void PruneDeadAndTickAlive(float deltaTime)
        {
            for (var i = Enemies.Count - 1; i >= 0; i--)
            {
                var enemy = Enemies[i];
                if (enemy.IsReadyToBeDisposed)
                {
                    Enemies.RemoveAt(i);
                    enemy.Destroy();
                    continue;
                }
                enemy.Tick(deltaTime);
            }
        }
    }
}
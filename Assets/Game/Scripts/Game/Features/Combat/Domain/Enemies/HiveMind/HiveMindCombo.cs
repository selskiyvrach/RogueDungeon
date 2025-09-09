using System.Collections.Generic;
using System.Linq;
using Libs.Lifecycle;
using Libs.Utils.DotNet;

namespace Game.Features.Combat.Domain.Enemies.HiveMind
{
    public class HiveMindCombo : ITickable
    {
        private readonly HiveMind _hiveMind;
        private readonly List<Enemy> _enemies = new(3);
        private int _currentEnemyIndex;
        private float _currentChillTime;
        
        public bool IsRunning { get; private set; }
        private EnemyCombo _currentCombo => _enemies[_currentEnemyIndex].CurrentCombo;

        public HiveMindCombo(HiveMind hiveMind) => 
            _hiveMind = hiveMind;

        public void Tick(float timeDelta)
        {
            if (!IsRunning)
            {
                var targetChillTime = _hiveMind.Enemies.Sum(n => n.Config.ChillTime) / _hiveMind.Enemies.Count;
                _currentChillTime += timeDelta;
                if (_currentChillTime >= targetChillTime && _enemies.All(n => !n.IsMoving)) 
                    StartCombo();
                else
                    return;
            }

            if (_currentCombo.IsRunning) 
                return;
            
            if (++_currentEnemyIndex >= _enemies.Count)
            {
                Stop();
                return;
            }
            // if enemy is unable to start a combo (is staggered for example) it will not be running the next frame and the next enemy will be picked 
            _currentCombo.StartNewCombo();
        }

        private void StartCombo()
        {
            _enemies.Clear();
            _enemies.AddRange(_hiveMind.Enemies);
            _enemies.Shuffle();
            _currentEnemyIndex = 0;
            _currentChillTime = 0;
            _currentCombo.StartNewCombo();
            IsRunning = true;
        }

        private void Stop()
        {
            IsRunning = false;
            _enemies.Clear();
        }
    }
}
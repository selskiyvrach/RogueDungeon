using System;
using System.Collections.Generic;
using RogueDungeon.Enemies;
using RogueDungeon.Enemies.HiveMind;
using RogueDungeon.Player;

namespace RogueDungeon.Combat
{
    public class CombatantsRegistry : IEnemiesRegistry, IPlayerRegistry
    {
        private readonly List<Enemy> _enemies = new(3);
        private Player.Player _player;

        public Player.Player Player
        {
            get => _player;
            set
            {
                if (_player != null)
                    throw new Exception("Another player instance is already registered.");
                _player = value;
            }
        }

        public IEnumerable<Enemy> Enemies => _enemies;

        public void RegisterEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
                throw new Exception("This enemy instance is already registered.");
            _enemies.Add(enemy);
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            if(!_enemies.Remove(enemy))
                throw new ArgumentException("Enemy is not registered");
        }
    }
}
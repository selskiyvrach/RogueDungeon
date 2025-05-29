using System;
using System.Collections.Generic;
using Enemies;
using Player.Model;

namespace Combat
{
    public class CombatantsRegistry : IEnemiesRegistry, IPlayerRegistry
    {
        private Player.Model.PlayerModel _player;

        public Player.Model.PlayerModel Player
        {
            get => _player;
            set
            {
                if (_player != null)
                    throw new Exception("Another player instance is already registered.");
                _player = value;
            }
        }

        public List<Enemy> Enemies { get; } = new(3);

        public void RegisterEnemy(Enemy enemy)
        {
            if (Enemies.Contains(enemy))
                throw new Exception("This enemy instance is already registered.");
            Enemies.Add(enemy);
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            if(!Enemies.Remove(enemy))
                throw new ArgumentException("Enemy is not registered");
        }
    }
}
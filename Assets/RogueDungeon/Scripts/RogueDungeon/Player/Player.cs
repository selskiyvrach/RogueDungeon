using RogueDungeon.Combat;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class Player : IDodger, IPlayerCombatant 
    {
        private readonly PlayerHandsBehaviour _playerHandsBehaviour;
        public PlayerGameObject GameObject { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public Vector2Int LevelCoordinates { get; set; }

        public Player(PlayerHandsBehaviour playerHandsBehaviour, PlayerGameObject gameObject)
        {
            _playerHandsBehaviour = playerHandsBehaviour;
            GameObject = gameObject;
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
    
    // create enemy class
    // installer
    // factory
    // spawner
    // install factory
    // create an enemy in gameplay class
}
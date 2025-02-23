using RogueDungeon.Combat;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;
using RogueDungeon.Player.Behaviours.Movement;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class Player : IDodger, IPlayerCombatant, ILevelTraverser 
    {
        private readonly PlayerHandsBehaviour _playerHandsBehaviour;
        public PlayerGameObject GameObject { get; }
        public PlayerDodgeState DodgeState { get; set; }

        Vector2 ILevelTraverser.Position
        {
            get => GameObject.transform.position;
            set => GameObject.transform.position = value;
        }

        Vector2 ILevelTraverser.Direction
        {
            get => GameObject.transform.forward;
            set => GameObject.transform.forward = value;
        }

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
}
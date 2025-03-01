using System;
using Common.Unity;
using RogueDungeon.Combat;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Levels;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;
using RogueDungeon.Player.Behaviours.Movement;
using UnityEngine;
using Behaviour = Common.Behaviours.Behaviour;

namespace RogueDungeon.Player
{
    public class Player : Behaviour, IDodger, IPlayerCombatant
    {
        private readonly LevelTraverserBehaviour _levelTraverserBehaviour;
        private readonly PlayerConfig _config;
        private readonly PlayerHandsBehaviour _playerHandsBehaviour;
        public Transform CameraPovPoint { get; }
        public TwoDWorldObject WorldObject { get; }
        public PlayerDodgeState DodgeState { get; set; }
        public bool IsAlive { get; }

        public Player(PlayerConfig config, PlayerHandsBehaviour playerHandsBehaviour, PlayerGameObject gameObject, LevelTraverserBehaviour levelTraverserBehaviour)
        {
            _config = config;
            _playerHandsBehaviour = playerHandsBehaviour;
            _levelTraverserBehaviour = levelTraverserBehaviour;
            WorldObject = new TwoDWorldObject(gameObject.gameObject);
            CameraPovPoint = gameObject.CameraReferencePoint;
            _levelTraverserBehaviour.LevelTraverser = WorldObject;
        }

        public override void Enable()
        {
            base.Enable();
            _levelTraverserBehaviour.Enable();
            _playerHandsBehaviour.Enable();
            ((IHandheldContext)_playerHandsBehaviour).IntendedItem = new Item(_config.DefaultWeapon); 
        }

        public override void Disable()
        {
            base.Disable();
            _levelTraverserBehaviour.Disable();
            _playerHandsBehaviour.Disable();
        }

        public void TakeDamage(float damage)
        {
            Debug.LogError("Player taking damage");
        }
    }
}
using System;
using RogueDungeon.Combat;
using RogueDungeon.Player.Behaviours.Dodge;
using RogueDungeon.Player.Behaviours.Hands;
using RogueDungeon.Player.Behaviours.Movement;
using UnityEngine;
using Behaviour = Common.Behaviours.Behaviour;

namespace RogueDungeon.Player
{
    public class Player : Behaviour, IDodger, IPlayerCombatant, ILevelTraverser 
    {
        private readonly LevelTraverserBehaviour _levelTraverserBehaviour;
        private readonly PlayerHandsBehaviour _playerHandsBehaviour;
        public PlayerGameObject GameObject { get; }
        public PlayerDodgeState DodgeState { get; set; }

        Vector2 ILevelTraverser.Position
        {
            get
            {
                var pos = GameObject.transform.localPosition;
                return new Vector2(pos.x, pos.z);
            }
            set => GameObject.transform.localPosition = new Vector3(value.x, 0, value.y);
        }

        Vector2 ILevelTraverser.Direction
        {
            get
            {
                var dir = GameObject.transform.forward;
                return new Vector2(dir.x, dir.z);
            }
            set => GameObject.transform.forward = new Vector3(value.x, 0, value.y);
        }

        public Player(PlayerHandsBehaviour playerHandsBehaviour, PlayerGameObject gameObject, LevelTraverserBehaviour levelTraverserBehaviour)
        {
            _playerHandsBehaviour = playerHandsBehaviour;
            GameObject = gameObject;
            _levelTraverserBehaviour = levelTraverserBehaviour;
            _levelTraverserBehaviour.LevelTraverser = this;
        }

        public override void Enable()
        {
            base.Enable();
            _levelTraverserBehaviour.Enable();
            _playerHandsBehaviour.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            _levelTraverserBehaviour.Disable();
            _playerHandsBehaviour.Disable();
        }

        public void TakeDamage(float damage)
        {
            throw new NotImplementedException();
        }
    }
}
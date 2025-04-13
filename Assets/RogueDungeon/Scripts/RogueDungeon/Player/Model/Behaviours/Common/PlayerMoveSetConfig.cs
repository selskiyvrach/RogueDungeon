using System;
using System.Collections.Generic;
using Common.Animations;
using Common.MoveSets;
using UnityEngine;
using UnityEngine.Serialization;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    [Serializable]
    public class PlayerMoveSetConfig : IMoveSetConfig
    {
        [SerializeField] private AnimationClipAdapterConfig _idleAnimation;
        [SerializeField] private AnimationClipAdapterConfig _birthAnimation;
        [SerializeField] private AnimationClipAdapterConfig _deathAnimation;
        [SerializeField] private AnimationClipAdapterConfig _walkAnimation;
        [SerializeField] private AnimationClipAdapterConfig _dodgeLeftAnimation;
        [SerializeField] private AnimationClipAdapterConfig _dodgeRightAnimation;
        
        public string FirstMoveId => Names.BIRTH;
        public IEnumerable<MoveCreationArgs> MovesCreationArgs => new MoveCreationArgs[]
        {
            new (Names.BIRTH, typeof(BirthMove), _birthAnimation, new TransitionPicker[] { new(Names.IDLE)}),
            new (Names.IDLE, typeof(IdleMove), _idleAnimation, new TransitionPicker[]
            {
                new(Names.DEATH, canInterrupt: true),
                new(Names.MOVE_FORWARD, canInterrupt: true),
                new(Names.TURN_LEFT, canInterrupt: true),
                new(Names.TURN_RIGHT, canInterrupt: true),
                new(Names.TURN_AROUND, canInterrupt: true),
                new(Names.DODGE_LEFT, canInterrupt: true),
                new(Names.DODGE_RIGHT, canInterrupt: true),
            }),
            new (Names.DEATH, typeof(DeathMove), _deathAnimation, Array.Empty<TransitionPicker>()),
            
            new (Names.DODGE_LEFT, typeof(DodgeLeftMove), _dodgeLeftAnimation, new TransitionPicker[] { new(Names.IDLE)}),
            new (Names.DODGE_RIGHT, typeof(DodgeRightMove), _dodgeRightAnimation, new TransitionPicker[] { new(Names.IDLE)}),
            
            new (Names.MOVE_FORWARD, typeof(MoveForwardMove), _walkAnimation, MovementTransitions),
            new (Names.TURN_LEFT, typeof(TurnLeftMove), _walkAnimation, MovementTransitions),
            new (Names.TURN_RIGHT, typeof(TurnRightMove), _walkAnimation, MovementTransitions),
            new (Names.TURN_AROUND, typeof(TurnAroundMove), _walkAnimation, MovementTransitions),
        };

        private static TransitionPicker[] MovementTransitions = {
            new (Names.TURN_LEFT),
            new (Names.TURN_RIGHT),
            new (Names.TURN_AROUND),
            new (Names.MOVE_FORWARD),
            new (Names.IDLE),
        };
    }
}
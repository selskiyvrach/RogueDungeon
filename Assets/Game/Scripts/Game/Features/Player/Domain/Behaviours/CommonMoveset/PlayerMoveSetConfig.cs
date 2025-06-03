using System;
using System.Collections.Generic;
using Libs.Animations;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class PlayerMoveSetConfig : TransformAnimationsConfig, IMoveSetConfig
    {
        public static class Names
        {
            public const string IDLE = "idle";
            public const string BIRTH = "birth";
            public const string DEATH = "death";
            public const string DODGE_LEFT = "dodge_left";
            public const string DODGE_RIGHT = "dodge_right";
            public const string MOVE_FORWARD = "move_forward";
            public const string TURN_LEFT = "turn_left";
            public const string TURN_RIGHT = "turn_right";
            public const string TURN_AROUND = "turn_around";
            public const string INVENTORY_OPEN = "inventory_open";
            public const string INVENTORY_KEEP_OPEN = "inventory_keep_open";
            public const string INVENTORY_CLOSE = "inventory_close";
        }
        [SerializeField, HideLabel, BoxGroup(nameof(_toIdleAnimation))] private TransformAnimationConfig _toIdleAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_hideHandsAnimation))] private TransformAnimationConfig _hideHandsAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_showHandsAnimation))] private TransformAnimationConfig _showHandsAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_birthAnimation))] private TransformAnimationConfig _birthAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_deathAnimation))] private TransformAnimationConfig _deathAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_idleAnimation))] private TransformAnimationConfig _idleAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_handsIdleAnimation))] private TransformAnimationConfig _handsIdleAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_walkAnimation))] private TransformAnimationConfig _walkAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_handsWalkAnimation))] private TransformAnimationConfig _handsWalkAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_turnAnimation))] private TransformAnimationConfig _turnAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_handsTurnAnimation))] private TransformAnimationConfig _handsTurnAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_dodgeLeftAnimation))] private TransformAnimationConfig _dodgeLeftAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_handsDodgeLeftAnimation))] private TransformAnimationConfig _handsDodgeLeftAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_dodgeRightAnimation))] private TransformAnimationConfig _dodgeRightAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_handsDodgeRightAnimation))] private TransformAnimationConfig _handsDodgeRightAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_openInventoryAnimation))] private TransformAnimationConfig _openInventoryAnimation;
        [SerializeField, HideLabel, BoxGroup(nameof(_keepInventoryOpenAnimation))] private TransformAnimationConfig _keepInventoryOpenAnimation;
        
        public string FirstMoveId => Names.BIRTH;
        public IEnumerable<MoveCreationArgs> MovesCreationArgs => new MoveCreationArgs[]
        {
            new (Names.BIRTH, typeof(BirthMove), _birthAnimation, new TransitionPicker[] { new(Names.IDLE)}),
            new (Names.IDLE, typeof(IdleMove), new MultiItemTransformAnimationConfig((null, _idleAnimation), ("hands", _handsIdleAnimation)), new TransitionPicker[]
            {
                new(Names.DEATH, canInterrupt: true),
                new(Names.INVENTORY_OPEN, canInterrupt: true),
                new(Names.MOVE_FORWARD, canInterrupt: true),
                new(Names.TURN_LEFT, canInterrupt: true),
                new(Names.TURN_RIGHT, canInterrupt: true),
                new(Names.TURN_AROUND, canInterrupt: true),
                new(Names.DODGE_LEFT, canInterrupt: true),
                new(Names.DODGE_RIGHT, canInterrupt: true),
            }),
            new (Names.DEATH, typeof(DeathMove), new MultiItemTransformAnimationConfig((null, _deathAnimation), ("hands", _hideHandsAnimation)), Array.Empty<TransitionPicker>()),
            
            new (Names.DODGE_LEFT, typeof(DodgeLeftMove), new MultiItemTransformAnimationConfig((null, _dodgeLeftAnimation), ("hands", _handsDodgeLeftAnimation)), new TransitionPicker[] { new(Names.IDLE)}),
            new (Names.DODGE_RIGHT, typeof(DodgeRightMove), new MultiItemTransformAnimationConfig((null, _dodgeRightAnimation), ("hands", _handsDodgeRightAnimation)), new TransitionPicker[] { new(Names.IDLE)}),
            
            new (Names.MOVE_FORWARD, typeof(MoveForwardMove), new MultiItemTransformAnimationConfig((null, _walkAnimation), ("hands", _handsWalkAnimation)), MovementTransitions),
            new (Names.TURN_LEFT, typeof(TurnLeftMove), new MultiItemTransformAnimationConfig((null, _turnAnimation), ("hands", _handsTurnAnimation)), MovementTransitions),
            new (Names.TURN_RIGHT, typeof(TurnRightMove), new MultiItemTransformAnimationConfig((null, _turnAnimation), ("hands", _handsTurnAnimation)), MovementTransitions),
            new (Names.TURN_AROUND, typeof(TurnAroundMove), new MultiItemTransformAnimationConfig((null, _walkAnimation), ("hands", _handsWalkAnimation)), MovementTransitions),
            
            new (Names.INVENTORY_OPEN, typeof(InventoryOpenMove), new MultiItemTransformAnimationConfig((null, _openInventoryAnimation), ("hands", _hideHandsAnimation)), new TransitionPicker[]{new(Names.INVENTORY_KEEP_OPEN)}),
            new (Names.INVENTORY_KEEP_OPEN, typeof(InventoryKeepOpenMove), _keepInventoryOpenAnimation, new TransitionPicker[]{new(Names.INVENTORY_CLOSE)}),
            new (Names.INVENTORY_CLOSE, typeof(InventoryCloseMove),  new MultiItemTransformAnimationConfig((null, _toIdleAnimation), ("hands", _showHandsAnimation)), new TransitionPicker[]{new(Names.IDLE)}),
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
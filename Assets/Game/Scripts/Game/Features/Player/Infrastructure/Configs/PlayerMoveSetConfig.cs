using System;
using System.Collections.Generic;
using Game.Features.Player.Domain.Movesets.Movement;
using Libs.Animations;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class PlayerMoveIdToTypeConverter : IMoveIdToTypeConverter
    {
        public Type GetMoveType(string id) =>
            id switch
            {
                MoveNames.IDLE => typeof(IdleMove),
                MoveNames.BIRTH => typeof(BirthMove),
                MoveNames.DEATH => typeof(DeathMove),
                MoveNames.DODGE_LEFT => typeof(DodgeLeftMove),
                MoveNames.DODGE_RIGHT => typeof(DodgeRightMove),
                MoveNames.MOVE_FORWARD => typeof(MoveForwardMove),
                MoveNames.TURN_LEFT => typeof(TurnLeftMove),
                MoveNames.TURN_RIGHT => typeof(TurnRightMove),
                MoveNames.TURN_AROUND => typeof(TurnAroundMove),
                MoveNames.INVENTORY_OPEN => typeof(InventoryOpenMove),
                MoveNames.INVENTORY_KEEP_OPEN => typeof(InventoryKeepOpenMove),
                MoveNames.INVENTORY_CLOSE => typeof(InventoryCloseMove),
                _ => throw new ArgumentException($"Unknown move id: {id}", nameof(id))
            };
    }

    public class PlayerMoveSetConfig : TransformAnimationsConfig, IMoveSetConfig
    {
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
        
        public string FirstMoveId => MoveNames.BIRTH;
        public IEnumerable<MoveCreationArgs> MovesCreationArgs => new MoveCreationArgs[]
        {
            new (MoveNames.BIRTH, _birthAnimation, new TransitionPicker[] { new(MoveNames.IDLE)}),
            new (MoveNames.IDLE, new MultiItemTransformAnimationConfig((null, _idleAnimation), ("hands", _handsIdleAnimation)), new TransitionPicker[]
            {
                new(MoveNames.DEATH, canInterrupt: true),
                new(MoveNames.INVENTORY_OPEN, canInterrupt: true),
                new(MoveNames.MOVE_FORWARD, canInterrupt: true),
                new(MoveNames.TURN_LEFT, canInterrupt: true),
                new(MoveNames.TURN_RIGHT, canInterrupt: true),
                new(MoveNames.TURN_AROUND, canInterrupt: true),
                new(MoveNames.DODGE_LEFT, canInterrupt: true),
                new(MoveNames.DODGE_RIGHT, canInterrupt: true),
            }),
            new (MoveNames.DEATH, new MultiItemTransformAnimationConfig((null, _deathAnimation), ("hands", _hideHandsAnimation)), Array.Empty<TransitionPicker>()),
            
            new (MoveNames.DODGE_LEFT, new MultiItemTransformAnimationConfig((null, _dodgeLeftAnimation), ("hands", _handsDodgeLeftAnimation)), new TransitionPicker[] { new(MoveNames.IDLE)}),
            new (MoveNames.DODGE_RIGHT, new MultiItemTransformAnimationConfig((null, _dodgeRightAnimation), ("hands", _handsDodgeRightAnimation)), new TransitionPicker[] { new(MoveNames.IDLE)}),
            
            new (MoveNames.MOVE_FORWARD, new MultiItemTransformAnimationConfig((null, _walkAnimation), ("hands", _handsWalkAnimation)), MovementTransitions),
            new (MoveNames.TURN_LEFT, new MultiItemTransformAnimationConfig((null, _turnAnimation), ("hands", _handsTurnAnimation)), MovementTransitions),
            new (MoveNames.TURN_RIGHT, new MultiItemTransformAnimationConfig((null, _turnAnimation), ("hands", _handsTurnAnimation)), MovementTransitions),
            new (MoveNames.TURN_AROUND, new MultiItemTransformAnimationConfig((null, _walkAnimation), ("hands", _handsWalkAnimation)), MovementTransitions),
            
            new (MoveNames.INVENTORY_OPEN, new MultiItemTransformAnimationConfig((null, _openInventoryAnimation), ("hands", _hideHandsAnimation)), new TransitionPicker[]{new(MoveNames.INVENTORY_KEEP_OPEN)}),
            new (MoveNames.INVENTORY_KEEP_OPEN, _keepInventoryOpenAnimation, new TransitionPicker[]{new(MoveNames.INVENTORY_CLOSE)}),
            new (MoveNames.INVENTORY_CLOSE, new MultiItemTransformAnimationConfig((null, _toIdleAnimation), ("hands", _showHandsAnimation)), new TransitionPicker[]{new(MoveNames.IDLE)}),
        };

        private static TransitionPicker[] MovementTransitions = {
            new (MoveNames.TURN_LEFT),
            new (MoveNames.TURN_RIGHT),
            new (MoveNames.TURN_AROUND),
            new (MoveNames.MOVE_FORWARD),
            new (MoveNames.IDLE),
        };
    }
}
using RogueDungeon.Characters;
using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Actions
{
    public class BlockAction : Action
    {
        private readonly Character _character;
        private readonly BlockingWeaponConfig _blockingWeapon;
        private bool _canLowerIfNotHoldingInput;
        private bool _lowerBlockCommandReceived;
        public override bool IsFinished => IsRewinding && CurrentFrame == 1;

        public BlockAction(Character character, BlockingWeaponConfig blockingWeapon) : base(blockingWeapon.BlockActionConfig)
        {
            _character = character;
            _blockingWeapon = blockingWeapon;
        }

        protected override void OnStarted()
        {
            _canLowerIfNotHoldingInput = false;
            _lowerBlockCommandReceived = false;
        }

        public override void OnCommand(string command)
        {
            switch (command)
            {
                case "LowerBlock":
                    if (_canLowerIfNotHoldingInput && !IsRewinding)
                        IsRewinding = true;
                    else
                        _lowerBlockCommandReceived = true;
                    break;
            }
        }

        protected override void OnKeyframe(string keyframe)
        {
            var combatState = _character.CombatState;
            switch (keyframe)
            {
                case "BlockRaised" when !IsRewinding:
                    RaiseBlock(combatState);
                    break;
                case "BlockRaised" when IsRewinding:
                    LowerBlock(combatState);
                    break;
                case "CanLowerIfNotHoldingInput":
                    if (_lowerBlockCommandReceived && !IsRewinding)
                        IsRewinding = true;
                    else
                        _canLowerIfNotHoldingInput = true;
                    break;
                default:
                    Debug.LogError("Invalid keyframe in block action: " + keyframe);
                    break;
            }
        }

        private void RaiseBlock(CombatState combatState)
        {
            combatState.BlockIsRaised = true;
            combatState.BlockingWeaponStats = _blockingWeapon;
        }

        private static void LowerBlock(CombatState combatState)
        {
            combatState.BlockIsRaised = false;
            combatState.BlockingWeaponStats = null;
        }

        protected override void OnStop() => 
            LowerBlock(_character.CombatState);
    }
}
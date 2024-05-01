using RogueDungeon.Characters;
using RogueDungeon.Data;
using RogueDungeon.Items;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Actions
{
    public class BlockAction : Action
    {
        private readonly BlockConfig _block;
        private bool _canLowerIfNotHoldingInput;
        private bool _lowerBlockCommandReceived;
        public override bool IsFinished => IsRewinding && CurrentFrame == 1;

        public BlockAction(BlockConfig block, StandardValues standardValues) : base(block.BlockActionConfig, standardValues) => 
            _block = block;

        protected override void OnStarted()
        {
            _canLowerIfNotHoldingInput = false;
            _lowerBlockCommandReceived = false;
        }

        public override void OnCommand(string command)
        {
            Assert.IsTrue(command == "LowerBlock");
            if(command != "LowerBlock")
                return;
            
            if (_canLowerIfNotHoldingInput && !IsRewinding)
                IsRewinding = true;
            else
                _lowerBlockCommandReceived = true;
        }

        protected override void OnKeyframe(string keyframe)
        {
            var combatState = Character.CombatState;
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
            combatState.BlockingWeaponStats = _block;
        }

        private static void LowerBlock(CombatState combatState)
        {
            combatState.BlockIsRaised = false;
            combatState.BlockingWeaponStats = null;
        }

        protected override void OnStop() => 
            LowerBlock(Character.CombatState);
    }
}
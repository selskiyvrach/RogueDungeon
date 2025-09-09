using System;
using Game.Features.Player.Domain.Movesets.Items;
using Game.Libs.Items;
using Libs.Movesets;
using ItemMoveNames = Game.Libs.Items.MoveNames;

namespace Game.Features.Player.Infrastructure.Configs
{
    public class ItemMoveIdToTypeConverter : IMoveIdToTypeConverter
    {
        public Type GetMoveType(string id) =>
            id switch
            {
                ItemMoveNames.UNSHEATH => typeof(UnsheathMove),
                ItemMoveNames.IDLE => typeof(ItemIdleMove),
                ItemMoveNames.SHEATH => typeof(SheathMove),

                ItemMoveNames.BLOCK_RAISE => typeof(ItemRaiseBlockMove),
                ItemMoveNames.BLOCK_HOLD => typeof(ItemHoldBlockMove),
                ItemMoveNames.BLOCK_LOWER => typeof(ItemLowerBlockMove),
                ItemMoveNames.BLOCK_ABSORB_IMPACT => typeof(ItemAbsorbBlockImpactMove),

                MoveTypeIds.ATTACK_PREPARE => typeof(ItemPrepareAttackMove),
                MoveTypeIds.ATTACK_EXECUTE => typeof(ItemExecuteAttackMove),
                MoveTypeIds.ATTACK_TRANSITION => typeof(ItemTransitionBetweenAttacksMove),
                MoveTypeIds.ATTACK_RECOVER => typeof(ItemRecoverAttackMove),

                ItemMoveNames.MAP_RAISE => typeof(RaiseMapMove),
                ItemMoveNames.MAP_HOLD => typeof(HoldMapMove),
                ItemMoveNames.MAP_LOWER => typeof(LowerMapMove),

                _ => throw new ArgumentException($"Unknown move id: {id}", nameof(id))
            };
    }
}
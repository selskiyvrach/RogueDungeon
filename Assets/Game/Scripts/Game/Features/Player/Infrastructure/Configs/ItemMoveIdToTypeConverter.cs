using System;
using Game.Features.Player.Domain.Movesets.Items;
using Game.Libs.Items;
using Libs.Movesets;

namespace Game.Features.Player.Infrastructure.Configs
{
    public class ItemMoveIdToTypeConverter : IMoveIdToTypeConverter
    {
        public Type GetMoveType(string id) =>
            id switch
            {
                MoveNames.UNSHEATH => typeof(UnsheathMove),
                MoveNames.IDLE => typeof(ItemIdleMove),
                MoveNames.SHEATH => typeof(SheathMove),

                MoveNames.BLOCK_RAISE => typeof(ItemRaiseBlockMove),
                MoveNames.BLOCK_HOLD => typeof(ItemHoldBlockMove),
                MoveNames.BLOCK_LOWER => typeof(ItemLowerBlockMove),
                MoveNames.BLOCK_ABSORB_IMPACT => typeof(ItemAbsorbBlockImpactMove),

                MoveTypeIds.ATTACK_PREPARE => typeof(ItemPrepareAttackMove),
                MoveTypeIds.ATTACK_EXECUTE => typeof(ItemExecuteAttackMove),
                MoveTypeIds.ATTACK_TRANSITION => typeof(ItemTransitionBetweenAttacksMove),
                MoveTypeIds.ATTACK_RECOVER => typeof(ItemRecoverAttackMove),

                MoveNames.MAP_RAISE => typeof(RaiseMapMove),
                MoveNames.MAP_HOLD => typeof(HoldMapMove),
                MoveNames.MAP_LOWER => typeof(LowerMapMove),

                _ => throw new ArgumentException($"Unknown move id: {id}", nameof(id))
            };
    }
}
using RogueDungeon.Characters;
using RogueDungeon.Data;
using RogueDungeon.Items;

namespace RogueDungeon.Actions
{
    public class ActionFactory
    {
        private readonly StandardValues _standardValues;

        public ActionFactory(StandardValues standardValues) => 
            _standardValues = standardValues;

        public AttackAction CreateAttackAction(AttackConfig config) => 
            new(config, _standardValues);
        public BlockAction CreateBlockAction(BlockConfig config) => 
            new(config, _standardValues);
        public DodgeAction CreateDodgeAction(ActionConfig config, DodgeState dodgeState) =>
            new(config, dodgeState, _standardValues);
    }
}
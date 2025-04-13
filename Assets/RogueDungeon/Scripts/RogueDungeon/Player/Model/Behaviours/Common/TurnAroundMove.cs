using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Levels;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class TurnAroundMove : TurnMove
    {
        protected override InputKey RequiredKey => InputKey.TurnAround;
        protected override float RotationDegrees => 180;

        public TurnAroundMove(Player player, Level level, IPlayerInput playerInput, IAnimation animation) : base(player, level, playerInput, animation, Names.TURN_AROUND)
        {
        }
    }
}
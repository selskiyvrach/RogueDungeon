using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Levels;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class TurnRightMove : TurnMove
    {
        protected override InputKey RequiredKey => InputKey.TurnRight;
        protected override float RotationDegrees => 90;

        public TurnRightMove(Player player, Level level, IPlayerInput playerInput, IAnimation animation) : base(player, level, playerInput, animation, Names.TURN_RIGHT)
        {
        }
    }
}
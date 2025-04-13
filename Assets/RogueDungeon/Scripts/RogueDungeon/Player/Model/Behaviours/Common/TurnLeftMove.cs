using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Levels;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class TurnLeftMove : TurnMove
    {
        protected override InputKey RequiredKey => InputKey.TurnLeft;
        protected override float RotationDegrees => -90;

        public TurnLeftMove(Player player, Level level, IPlayerInput playerInput, IAnimation animation) : base(player, level, playerInput, animation, Names.TURN_LEFT)
        {
        }
    }
}
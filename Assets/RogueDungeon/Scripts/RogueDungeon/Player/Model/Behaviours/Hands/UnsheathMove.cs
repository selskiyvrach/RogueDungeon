using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class UnsheathMove : HandsState
    {
        private readonly PlayerHandsBehaviour _hands;
        private readonly PlayerHandBehaviour _handBehaviour;

        public UnsheathMove(PlayerHandBehaviour handBehaviour, HandHeldMoveConfig config, IAnimation animation, PlayerHandsBehaviour hands) : base(config, animation)
        {
            _handBehaviour = handBehaviour;
            _hands = hands;
        }
    }
}
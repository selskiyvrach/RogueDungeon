using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class RaiseMapMove : PlayerMove
    {
        private readonly IPlayerInput _input;
        private readonly Map _map;
        protected override float Duration => 1; //_map.Config.RaiseBlockDuration; 
        public RaiseMapMove(string id, IAnimation animation, Map map, IPlayerInput input) : base(id, animation)
        {
            _map = map;
            _input = input;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _input.IsDown(InputKey.UseRightHandItem);

        public override void Enter()
        {
            base.Enter();
            _input.ConsumeInput(InputKey.UseRightHandItem);
        }
    }
}
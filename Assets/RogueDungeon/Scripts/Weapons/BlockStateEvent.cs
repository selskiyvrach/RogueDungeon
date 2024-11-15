using RogueDungeon.Animations;

namespace RogueDungeon.Weapons
{
    public readonly struct BlockStateEvent : IAnimationEvent
    {
        public enum BlockState
        {
            Raised,
            Lowered,
        }

        public readonly BlockState State;

        public BlockStateEvent(BlockState state) => 
            State = state;
    }
}
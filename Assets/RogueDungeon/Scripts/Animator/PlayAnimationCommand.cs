namespace RogueDungeon.Animator
{
    public class PlayAnimationCommand : ICommand
    {
        private readonly string _animationName;
        private readonly Animator _animator;

        public PlayAnimationCommand(Animator animator, string animationName)
        {
            _animationName = animationName;
            _animator = animator;
        }

        public void Execute() => 
            _animator.PlayAnimation(_animationName);
    }
}
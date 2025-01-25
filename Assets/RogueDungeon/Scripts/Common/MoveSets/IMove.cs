namespace Common.MoveSets
{
    public interface IMove
    {
        string AnimationName {get;}
        float Duration { get; }
        IMove[] Transitions { get; }
    }
}
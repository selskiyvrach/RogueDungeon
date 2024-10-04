using UniRx;

namespace RogueDungeon
{
    public static class UnirxExtensions
    {
        public static void OnNext(this ISubject<Unit> subject)
            => subject.OnNext(Unit.Default);
    }
}
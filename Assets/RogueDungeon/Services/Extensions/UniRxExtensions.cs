using UniRx;

namespace RogueDungeon.Utils
{
    public static class UnirxExtensions
    {
        public static void OnNext(this ISubject<Unit> subject)
            => subject.OnNext(Unit.Default);
    }
}
using UniRx;

namespace RogueDungeon.Services.Extensions
{
    public static class UnirxExtensions
    {
        public static void OnNext(this ISubject<Unit> subject)
            => subject.OnNext(Unit.Default);
    }
}
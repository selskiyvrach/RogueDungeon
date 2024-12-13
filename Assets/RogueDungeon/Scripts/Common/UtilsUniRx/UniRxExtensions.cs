using UniRx;

namespace Common.UniRxUtils
{
    public static class UnirxExtensions
    {
        public static void OnNext(this ISubject<Unit> subject)
            => subject.OnNext(Unit.Default);
    }
}
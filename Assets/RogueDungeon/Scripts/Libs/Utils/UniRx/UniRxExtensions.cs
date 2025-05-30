using UniRx;

namespace Libs.Utils.UniRx
{
    public static class UnirxExtensions
    {
        public static void OnNext(this ISubject<Unit> subject)
            => subject.OnNext(Unit.Default);
    }
}
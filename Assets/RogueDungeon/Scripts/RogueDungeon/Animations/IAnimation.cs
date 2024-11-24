using UniRx;

namespace RogueDungeon.Animations
{
    public interface IAnimation
    {
        ISubject<string> OnEvent { get; }
        void Play();
        void Stop();
    }
}
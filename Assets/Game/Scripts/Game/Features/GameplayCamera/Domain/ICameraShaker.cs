namespace Game.Features.GameplayCamera.Domain
{
    public interface ICameraShaker
    {
        void DoShake(ShakeIntensity intensity);
    }
}
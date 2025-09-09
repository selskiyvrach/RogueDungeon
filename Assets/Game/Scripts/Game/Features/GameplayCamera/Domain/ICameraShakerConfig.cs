namespace Game.Features.GameplayCamera.Domain
{
    public interface ICameraShakerConfig
    {
        float GetShakeDuration(ShakeIntensity intensity);
        float GetShakeStrength(ShakeIntensity intensity);
    }
}
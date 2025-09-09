namespace Game.Features.VFX.App
{
    public interface ICameraShakerConfig
    {
        float GetShakeDuration(ShakeIntensity intensity);
        float GetShakeStrength(ShakeIntensity intensity);
    }
}
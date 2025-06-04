using Game.Features.Levels.Domain;

namespace Game.Features.Levels.App
{
    public interface ILevelFactory
    {
        Level Create(ILevelConfig config);
    }
}
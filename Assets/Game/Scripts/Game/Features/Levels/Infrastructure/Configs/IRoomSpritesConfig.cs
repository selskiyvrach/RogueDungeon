using Game.Features.Levels.Domain;
using UnityEngine;

namespace Game.Features.Levels.Infrastructure.Configs
{
    public interface IRoomSpritesConfig
    {
        Sprite GetSprite(AdjacencyType adjacencyType);
    }
}
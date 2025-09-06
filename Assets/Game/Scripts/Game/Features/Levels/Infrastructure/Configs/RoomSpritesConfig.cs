using System;
using System.Collections.Generic;
using System.Linq;
using Game.Features.Levels.Domain;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Levels.Infrastructure.Configs
{
    public class RoomSpritesConfig : ScriptableObject, IRoomSpritesConfig
    {
        [Serializable]
        private struct SpritePerAdjacency
        {
            [HorizontalGroup] public AdjacencyType Adjacency;
            [HorizontalGroup] public Sprite Sprite;
        }
        
        [SerializeField] private List<SpritePerAdjacency> _sprites;

        public Sprite GetSprite(AdjacencyType adjacencyType) => 
            _sprites.First(n => n.Adjacency == adjacencyType).Sprite;
    }
}
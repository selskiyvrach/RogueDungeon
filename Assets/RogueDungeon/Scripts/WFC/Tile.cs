using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.WFC
{
    [CreateAssetMenu(menuName = "Configs/WFC/Tile", fileName = "Tile", order = 0)]
    public class Tile : ScriptableObject
    {
        [field: PreviewField, SerializeField] public Sprite Sprite { get; private set; }
        [SerializeField] private Edge _exitsOnEdges;

        public bool Matches(Tile other, Edge edge)
        {
            Assert.IsTrue((edge & (edge - 1)) == 0, "Invalid argument! More than edge requested");
            return ExitExists(edge) == other.ExitExists(Opposite(edge));
        }

        public bool ExitExists(Edge edge) => 
            (_exitsOnEdges & edge) != 0;

        public static Edge Opposite(Edge source) =>
            source switch
            {
                Edge.Up => Edge.Down,
                Edge.Right => Edge.Left,
                Edge.Down => Edge.Up,
                Edge.Left => Edge.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(source), source, null),
            };

        public bool IsCorridor() => 
            IsHorizontalCorridor() || IsVerticalCorridor();

        public bool IsVerticalCorridor() => 
            _exitsOnEdges == (Edge.Up | Edge.Down);

        public bool IsHorizontalCorridor() => 
            _exitsOnEdges == (Edge.Left | Edge.Right);

        public bool Empty() => 
            _exitsOnEdges is Edge.None;

        public IEnumerable<(Vector2Int localCoords, bool state)> As3By3()
        {
            for (var i = 0; i < 9; i++)
            {
                yield return i switch
                {
                    0 => (new Vector2Int(-1, -1), false),
                    1 => (new Vector2Int(0, -1), (_exitsOnEdges & Edge.Down) != 0),
                    2 => (new Vector2Int(1, -1), false),
                    3 => (new Vector2Int(-1, 0), (_exitsOnEdges & Edge.Left) != 0),
                    4 => (new Vector2Int(0, 0), _exitsOnEdges != 0),
                    5 => (new Vector2Int(1, 0), (_exitsOnEdges & Edge.Right) != 0),
                    6 => (new Vector2Int(-1, 1), false),
                    7 => (new Vector2Int(0, 1), (_exitsOnEdges & Edge.Up) != 0),
                    8 => (new Vector2Int(1, 1), false),
                };
                
            }
        }
    }
}
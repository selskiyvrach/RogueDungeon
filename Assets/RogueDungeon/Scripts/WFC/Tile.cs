using System;
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

        private bool IsVerticalCorridor() => 
            _exitsOnEdges == (Edge.Up | Edge.Down);

        public bool IsHorizontalCorridor() => 
            _exitsOnEdges == (Edge.Left | Edge.Right);

        public bool Empty() => 
            _exitsOnEdges is Edge.None;
    }
}
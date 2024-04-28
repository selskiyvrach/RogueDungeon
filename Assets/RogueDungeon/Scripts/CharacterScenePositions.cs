using System;
using System.Linq;
using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon
{
    [CreateAssetMenu(menuName = "Configs/Characters/CharacterScenePositions", fileName = "RelativePositions", order = 0)]
    public class CharacterScenePositions : ScriptableObject
    {
        [Serializable]
        private struct PosTransform
        {
            public Position Position;
            public Vector3 RelativePos;
        }

        [SerializeField] private PosTransform[] _positions;

        public Vector3 GetRelativePosition(Position position) => 
            _positions.First(n => n.Position == position).RelativePos;
    }
}
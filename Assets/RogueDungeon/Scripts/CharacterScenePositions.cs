using System;
using System.Linq;
using RogueDungeon.Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace RogueDungeon
{
    [CreateAssetMenu(menuName = "Configs/Characters/CharacterScenePositions", fileName = "RelativePositions", order = 0)]
    public class CharacterScenePositions : ScriptableObject
    {
        [Serializable]
        private struct PosTransform
        {
            [FormerlySerializedAs("_positions")] [FormerlySerializedAs("Position")] public Position _position;
            public Vector3 RelativePos;
        }

        [SerializeField] private PosTransform[] _positions;

        public Vector3 GetRelativePosition(Position position) => 
            _positions.First(n => n._position == position).RelativePos;
    }
}
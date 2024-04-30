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
            [FormerlySerializedAs("Position")] public Positions _positions;
            public Vector3 RelativePos;
        }

        [SerializeField] private PosTransform[] _positions;

        public Vector3 GetRelativePosition(Positions positions) => 
            _positions.First(n => n._positions == positions).RelativePos;
    }
}
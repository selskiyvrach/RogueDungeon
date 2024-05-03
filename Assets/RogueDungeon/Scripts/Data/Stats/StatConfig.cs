using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace RogueDungeon.Data.Stats
{
    [Serializable]
    public class StatConfig
    {
        [HideLabel, SerializeField, HorizontalGroup(width: 0.3f)] 
        private RelativeValue _relativeValueType = RelativeValue.Custom;
        
        [field: HideLabel, HideIf("@_relativeValueType == RelativeValue.Custom"), ValidateInput("ValidateStandardValue", "This stat is not present in standard values"), SerializeField, HorizontalGroup]
        public string Key { get; private set; }
        
        [ShowIf("@_relativeValueType == RelativeValue.Custom"), HideLabel, SerializeField, HorizontalGroup] 
        private float _customValue;

        public float GetValue() => 
            _relativeValueType == RelativeValue.Custom ? _customValue : StandardValues.GetValue(Key, _relativeValueType);
        
        public int GetIntValue(RoundType roundType = RoundType.Round) =>
            roundType switch
            {
                RoundType.FloorToInt => Mathf.FloorToInt(GetValue()),
                RoundType.CeilToInt => Mathf.CeilToInt(GetValue()),
                RoundType.Round => Mathf.RoundToInt(GetValue()),
                _ => throw new ArgumentOutOfRangeException(nameof(roundType), roundType, null),
            };

        private bool ValidateStandardValue() => 
            StandardValues.HasStat(Key);
    }

    public enum RoundType
    {
        FloorToInt,
        CeilToInt,
        Round,
    }
}
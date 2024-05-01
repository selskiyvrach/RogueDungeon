using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace RogueDungeon.Data.Stats
{
    [Serializable]
    public class StatConfig
    {
        [field: HideLabel, SerializeField]
        public string Id { get; private set; }
        
        [HideLabel, SerializeField, ValidateInput("ValidateStandardValue", "This stat is not present in standard values"), HorizontalGroup] 
        private RelativeValue _relativeValueType = RelativeValue.Custom;
        
        [ShowIf("@_relativeValueType == RelativeValue.Custom"), HideLabel, SerializeField, HorizontalGroup] 
        private float _customValue;

        public float GetValue() => 
            _relativeValueType == RelativeValue.Custom ? _customValue : StandardValues.GetValue(Id, _relativeValueType);

        private bool ValidateStandardValue() => 
            _relativeValueType == RelativeValue.Custom || StandardValues.HasStat(Id);
    }
}
using System;

namespace Common.Prameters
{
    [Serializable]
    public class ParameterPicker<T> where T : struct, Enum
    {
        public T ParameterType;
        public Modifier.Type ModifierType = Modifier.Type.Flat;
        public float Value;
    }
}
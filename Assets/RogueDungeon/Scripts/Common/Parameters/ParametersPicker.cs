using System;
using Common.Keys;
using Common.UtilsUnity;

namespace Common.Parameters
{
    [Serializable]
    public class ParametersPicker<T> : SerializableDictionary<KeyPicker<T>, float>
    {
    }
}
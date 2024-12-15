using System;
using Common.UnityUtils;
using RogueDungeon.Parameters;

namespace Common.Parameters
{
    [Serializable]
    public class ParametersPicker<T> : SerializableDictionary<KeyPicker<T>, float>
    {
    }
}
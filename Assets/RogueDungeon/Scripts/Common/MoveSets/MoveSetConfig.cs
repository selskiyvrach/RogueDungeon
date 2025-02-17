using System.Collections.Generic;
using UnityEngine;

namespace Common.MoveSets
{
    public abstract class MoveSetConfig<T> : MoveSetConfig where T : MoveConfig
    {
        [field: SerializeField] public T[] MoveConfigs { get; private set; }

        public override IEnumerable<MoveConfig> Moves => MoveConfigs;
    }

    public abstract class MoveSetConfig : ScriptableObject
    {
        public abstract IEnumerable<MoveConfig> Moves { get; }
    }
}
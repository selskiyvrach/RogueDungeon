using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
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
        [field: SerializeField] public MoveSetConfig[] ExtendsConfigs { get; private set; }
        public string DebugName => name;
        public abstract IEnumerable<MoveConfig> Moves { get; }
        public virtual MoveConfig FirstMove => ExtendsConfigs.IsNullOrEmpty() ? Moves.First() : ExtendsConfigs.First().Moves.First();
    }
}
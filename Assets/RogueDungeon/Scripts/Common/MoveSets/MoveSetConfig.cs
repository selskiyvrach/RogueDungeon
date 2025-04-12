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

    public interface IMoveSetConfig
    {
        string FirstMoveId { get; }
        IEnumerable<MoveCreationArgs> MovesCreationArgs { get; }
    }

    public abstract class MoveSetConfig : ScriptableObject, IMoveSetConfig
    {
        [field: SerializeField] public MoveSetConfig[] ExtendsConfigs { get; private set; }
        public string DebugName => name;
        public virtual IEnumerable<MoveConfig> Moves { get; }
        public virtual string FirstMoveId => (ExtendsConfigs.IsNullOrEmpty() ? Moves.First() : ExtendsConfigs.First().Moves.First()).Id;
        public virtual IEnumerable<MoveCreationArgs> MovesCreationArgs => Moves.Select(n => n.ToCreationArgs());
    }
}
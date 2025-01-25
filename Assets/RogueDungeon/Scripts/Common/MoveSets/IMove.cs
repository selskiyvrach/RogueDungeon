using System.Collections.Generic;
using UnityEngine;

namespace Common.MoveSets
{
    public interface IMove
    {
        string Name {get;}
        AnimationClip Animation {get;}
        float Duration { get; }
        IEnumerable<IMove> Transitions { get; }
    }
}
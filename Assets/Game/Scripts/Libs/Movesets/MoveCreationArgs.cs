using System.Collections.Generic;
using Libs.Animations;
using Libs.Utils.DotNet;

namespace Libs.Movesets
{
    public readonly struct MoveCreationArgs
    {
        private readonly string _moveTypeId;
        
        public readonly string Id;
        public readonly string MoveTypeId => _moveTypeId.IsNullOrEmpty() ? Id : _moveTypeId;
        public readonly AnimationConfig AnimationConfig;
        public readonly IEnumerable<TransitionPicker> Transitions;

        /// <param name="moveTypeId"> An optional parameter for the cases when there's a few moves of the same type in a moveset hence their ids cannot match move type id</param>
        public MoveCreationArgs(string id, AnimationConfig animationConfig, IEnumerable<TransitionPicker> transitions, string moveTypeId = null)
        {
            Id = id;
            AnimationConfig = animationConfig;
            Transitions = transitions;
            _moveTypeId = moveTypeId;
        }
    }
}
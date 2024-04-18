using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon.Actions
{
    public class DodgeAction : Action
    {
        private readonly Character _character;
        private readonly DodgeState _dodgeState;

        public DodgeAction(Character character, DodgeState dodgeState, ActionConfig config) : base(config)
        {
            _character = character;
            _dodgeState = dodgeState;
        }

        protected override void OnKeyframe(string keyframe)
        {
            switch (keyframe)
            {
                case "DodgeStarted":
                    _character.CombatState.DodgeState = _dodgeState;
                    break;
                case "DodgeFinished":
                    _character.CombatState.DodgeState = DodgeState.NotDodging;
                    break;
                default:
                    Debug.LogError("Invalid keyframe in dodge action: " + keyframe);
                    break;
            }       
        }

        protected override void OnStop() => 
            _character.CombatState.DodgeState = DodgeState.NotDodging;
    }
}
using RogueDungeon.Characters;
using RogueDungeon.Data;
using UnityEngine;

namespace RogueDungeon.Actions
{
    public class DodgeAction : Action
    {
        private readonly DodgeState _dodgeState;

        public DodgeAction(ActionConfig config, DodgeState dodgeState) : base(config) => 
            _dodgeState = dodgeState;

        protected override void OnKeyframe(string keyframe)
        {
            switch (keyframe)
            {
                case "DodgeStarted":
                    Character.CombatState.DodgeState = _dodgeState;
                    break;
                case "DodgeFinished":
                    Character.CombatState.DodgeState = DodgeState.NotDodging;
                    break;
                default:
                    Debug.LogError("Invalid keyframe in dodge action: " + keyframe);
                    break;
            }       
        }

        protected override void OnStop() => 
            Character.CombatState.DodgeState = DodgeState.NotDodging;
    }
}
using UnityEngine;

namespace RogueDungeon.Characters
{
    public class KeyboardCharacterController : CharacterController
    {
        private readonly Character _character;
        private string _coyoteTimeCommand;
        private int _coyoteTimeFrames;

        public KeyboardCharacterController(Character character) : base(character) => 
            _character = character;

        public override void Tick()
        {
            if(Input.GetKeyDown(KeyCode.A))
                HandleInputCommand("DodgeLeft");
            if(Input.GetKeyDown(KeyCode.D))
                HandleInputCommand("DodgeRight");
            if(Input.GetKeyDown(KeyCode.Mouse1))
                HandleInputCommand("RaiseBlock");
            if(Input.GetKeyUp(KeyCode.Mouse1))
                HandleInputCommand("LowerBlock");
            if(Input.GetKeyDown(KeyCode.Mouse0))
                HandleInputCommand("Attack");
            
            base.Tick();
            
            if(_coyoteTimeCommand == null)
                return;
            
            if (_character.CurrentAction == null)
            {
                if (_coyoteTimeCommand == "RaiseBlockThenLower")
                {
                    OnCommand("RaiseBlock");
                    OnCommand("LowerBlock");
                }
                else
                    OnCommand(_coyoteTimeCommand);

                _coyoteTimeCommand = null;
                return;
            }

            if (_coyoteTimeFrames-- == 0)
                _coyoteTimeCommand = null;
        }

        private void HandleInputCommand(string command)
        {
            if (OnCommand(command)) 
                return;
            if (_coyoteTimeCommand == "RaiseBlock" && command == "LowerBlock")
                _coyoteTimeCommand = "RaiseBlockThenLower";
            else
                _coyoteTimeCommand = command;
            _coyoteTimeFrames = 15;
        }
    }
}
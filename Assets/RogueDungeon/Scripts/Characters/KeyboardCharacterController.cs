using UnityEngine;

namespace RogueDungeon.Characters
{
    public class KeyboardCharacterController
    {
        private readonly Character _character;
        private string _coyoteTimeCommand;
        private int _coyoteTimeFrames;

        public KeyboardCharacterController(Character character) => 
            _character = character;

        public void Tick()
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
            
            _character.Tick();
            
            if(_coyoteTimeCommand == null)
                return;
            
            if (_character.CurrentAction == null)
            {
                if (_coyoteTimeCommand == "RaiseBlockThenLower")
                {
                    _character.OnCommand("RaiseBlock");
                    _character.OnCommand("LowerBlock");
                }
                else
                    _character.OnCommand(_coyoteTimeCommand);

                _coyoteTimeCommand = null;
                return;
            }

            if (_coyoteTimeFrames-- == 0)
                _coyoteTimeCommand = null;
        }

        private void HandleInputCommand(string command)
        {
            if (_character.OnCommand(command)) 
                return;
            if (_coyoteTimeCommand == "RaiseBlock" && command == "LowerBlock")
                _coyoteTimeCommand = "RaiseBlockThenLower";
            else
                _coyoteTimeCommand = command;
            _coyoteTimeFrames = 15;
        }
    }
}
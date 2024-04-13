using UnityEngine;

namespace RogueDungeon.Characters
{
    public class KeyboardCharacterController
    {
        private readonly Character _character;

        public KeyboardCharacterController(Character character) => 
            _character = character;

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.A))
                _character.OnCommand("DodgeLeft");
            if(Input.GetKeyDown(KeyCode.D))
                _character.OnCommand("DodgeRight");
            if(Input.GetKeyDown(KeyCode.Mouse1))
                _character.OnCommand("RaiseBlock");
            if(Input.GetKeyUp(KeyCode.Mouse1))
                _character.OnCommand("LowerBlock");
            if(Input.GetKeyDown(KeyCode.Mouse0))
                _character.OnCommand("Attack");
            
            _character.Tick();
        }
    }
}
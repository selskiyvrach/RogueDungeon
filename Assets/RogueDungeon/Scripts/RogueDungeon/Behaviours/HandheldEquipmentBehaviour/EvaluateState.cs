using Common.Fsm;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class EvaluateState : IState
    {
        private readonly IChangingHandheldItemsInfo _equipment;
        
        public void Enter() { }

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if (_equipment.CurrentItem.Id != _equipment.IntendedItem.Id) 
                return;
            
            if(_equipment.CurrentItem == null)
                stateChanger.To<UnsheathState>();
            else
                stateChanger.To<SheathState>();
        }
    }
}
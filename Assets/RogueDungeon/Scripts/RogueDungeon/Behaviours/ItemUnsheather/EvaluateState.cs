using Common.Fsm;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class EvaluateState : IState
    {
        private readonly IChangingHandheldItemsInfo _equipment;
        
        public void CheckTransitions(IStateChanger stateChanger)
        {
            if (_equipment.CurrentItem == _equipment.IntendedItem) 
                return;
            
            if(_equipment.CurrentItem == null)
                stateChanger.To<UnsheathState>();
            else
                stateChanger.To<SheathState>();
        }
    }
}
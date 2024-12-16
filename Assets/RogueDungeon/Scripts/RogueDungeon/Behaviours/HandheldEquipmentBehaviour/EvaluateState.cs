using Common.Fsm;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class EvaluateState : IState
    {
        private readonly IChangingHandheldItemsInfo _equipment;
        
        public void CheckTransitions(IStateChanger stateChanger)
        {
            if (_equipment.CurrentItem.Item == _equipment.IntendedItem.Item) 
                return;
            
            if(_equipment.CurrentItem == null)
                stateChanger.To<UnsheathState>();
            else
                stateChanger.To<SheathState>();
        }
    }
}
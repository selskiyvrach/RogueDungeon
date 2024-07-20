namespace RogueDungeon.StateMachine
{
    public interface IStateTickHandler : IStateHandler
    {
        void OnTick();
    }
}
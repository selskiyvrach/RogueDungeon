namespace RogueDungeon.Services.FSM
{
    public interface IStateTickHandler : IStateHandler
    {
        void OnTick();
    }
}
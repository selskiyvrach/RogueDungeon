using System.Collections.Generic;

namespace RogueDungeon.StateMachine
{
    public interface IStateHandlersProvider
    {
        IEnumerable<IStateHandler> GetHandlers();
    }
}
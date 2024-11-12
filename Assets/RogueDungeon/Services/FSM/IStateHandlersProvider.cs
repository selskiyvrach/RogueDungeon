using System.Collections.Generic;

namespace RogueDungeon.Services.FSM
{
    public interface IStateHandlersProvider
    {
        IEnumerable<IStateHandler> GetHandlers();
    }
}
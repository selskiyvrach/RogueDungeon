using System.Collections.Generic;

namespace Common.FSM
{
    public interface IStateHandlersProvider
    {
        IEnumerable<IStateHandler> GetHandlers();
    }
}
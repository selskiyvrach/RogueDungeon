using System.Collections.Generic;

namespace Libs.Fsm
{
    public class IdBasedStatesProvider : IIdBasedStatesProvider
    {
        public Dictionary<string, IState> States { get; set; }
        
        public IState Get(string id) => 
            States.GetValueOrDefault(id);
    }
}
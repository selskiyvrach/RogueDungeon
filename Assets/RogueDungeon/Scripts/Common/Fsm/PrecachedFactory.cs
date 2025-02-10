using System;
using Common.UtilsDotNet;
using Zenject;

namespace Common.Fsm
{
    public class PrecachedProvider : StatesProviderWithCache
    {
        public PrecachedProvider(DiContainer container, Type[] statesToCreate) : base(container) => 
            statesToCreate.Foreach(Create);
    }
}
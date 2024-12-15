using System;
using Common.UtilsDotNet;
using Zenject;

namespace Common.Fsm
{
    public class PrecachedFactory : StatesFactoryWithCache
    {
        public PrecachedFactory(DiContainer container, Type[] statesToCreate) : base(container) => 
            statesToCreate.Foreach(Create);
    }
}
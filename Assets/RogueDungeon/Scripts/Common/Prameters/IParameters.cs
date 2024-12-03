using System;

namespace Common.Prameters
{
    public interface IParameters
    {
        Parameter Get<T>(T id) where T : struct, Enum;
        void Add<T>(T id, Parameter parameter) where T : struct, Enum;
    }
}
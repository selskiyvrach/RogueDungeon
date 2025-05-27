using System;

namespace Common.Pools
{
    public class PoolingException : Exception
    {
        public PoolingException(string message) : base(message)
        {
        }
    }
}
using System;

namespace Libs.Pools
{
    public class PoolingException : Exception
    {
        public PoolingException(string message) : base(message)
        {
        }
    }
}
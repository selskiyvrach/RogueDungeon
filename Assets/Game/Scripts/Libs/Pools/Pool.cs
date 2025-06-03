using System;
using System.Collections.Generic;
using System.Linq;

namespace Libs.Pools
{
    public class Pool<T> where T : class, IPoolable
    {
        private readonly Stack<T> _pool = new();
        private readonly Func<T> _factoryMethod;
        private readonly int _size;
        private int _currentSize;

        public Pool(Func<T> factoryMethod, int size)
        {
            _factoryMethod = factoryMethod;
            _size = size;
        }

        public Poolable<T> Get()
        {
            if (_pool.Any())
                return new Poolable<T>(this, _pool.Pop());
            if (_currentSize >= _size)
                throw new PoolingException("Exceeded pool size. Make sure you return items in time");
            _currentSize++;
            return new Poolable<T>(this, _factoryMethod());
        }

        public void ReturnItem(T item)
        {
            item.RecyclesCount++;
            item.Reset();
            _pool.Push(item);
        }
    }
}
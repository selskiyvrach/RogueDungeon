using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Pools
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

    public readonly struct Poolable<T> where T : class, IPoolable
    {
        private readonly Pool<T> _pool;
        private readonly T _item;
        private readonly int _itemRecyclesCount;

        public Poolable(Pool<T> pool, T item) : this()
        {
            _pool = pool;
            _item = item;
            _itemRecyclesCount = _item.RecyclesCount;
        }

        public T Item {
            get
            {
                ThrowIfItemIterationDiffers("Accessing an item that's been already returned to the pool");
                return _item;
            }
        }

        public void Release()
        {
            ThrowIfItemIterationDiffers("Releasing an item that's been already returned to the pool");
            _pool.ReturnItem(_item);
        }

        public bool IsItemValid(T item) => 
            item.RecyclesCount == _itemRecyclesCount;

        private void ThrowIfItemIterationDiffers(string message)
        {
            if (_itemRecyclesCount < _item.RecyclesCount)
                throw new PoolingException(message);
        }
    }

    public class PoolingException : Exception
    {
        public PoolingException(string message) : base(message)
        {
        }
    }

    public interface IPoolable
    {
        int RecyclesCount { get; set; }
        void Reset();
    }
}
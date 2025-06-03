namespace Libs.Pools
{
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
}
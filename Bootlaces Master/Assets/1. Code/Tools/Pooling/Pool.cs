namespace HighwayRage
{
    public class Pool<T> : IPool<T> where T : IPoolable<T>
    {
        private readonly PoolCore<T> _poolCore;
        
        public Pool(IPoolFactory<T> factory)
        {
            _poolCore = new PoolCore<T>(factory);
        }

        public Pool(IPoolFactory<T> factory, int prewarm)
        {
            _poolCore = new PoolCore<T>(factory, prewarm);
        }

        public T Get()
        {
            T poolable = _poolCore.Get();
            poolable.SpawnFrom(this);
            return poolable;
        }

        public void Return(T poolable)
        {
            _poolCore.Return(poolable);
        }

        public void ReturnAll()
        {
            _poolCore.ReturnAll();
        }
    }
}
using System.Collections.Generic;

namespace HighwayRage
{
    public class MultiPool<TKey, TPoolable> : IPool<TPoolable> where TPoolable : IPoolable<TPoolable>
    {
        private readonly Dictionary<TKey, PoolCore<TPoolable>> _pools = new Dictionary<TKey, PoolCore<TPoolable>>();

        private readonly Dictionary<TPoolable, TKey> _busyObjects = new Dictionary<TPoolable, TKey>();
        
        public void AddPool(TKey key, IPoolFactory<TPoolable> factory, int prewarm = 0)
        {
            _pools.Add(key, new PoolCore<TPoolable>(factory, prewarm));
        } 

        public void RemovePool(TKey key)
        {
            _pools.Remove(key);
        }

        public TPoolable Get(TKey key)
        {
            TPoolable poolable = _pools[key].Get();
            _busyObjects.Add(poolable, key);
            poolable.SpawnFrom(this);
            return poolable;
        }

        public void Return(TPoolable poolable)
        {
            TKey key = _busyObjects[poolable];
            _busyObjects.Remove(poolable);
            _pools[key].Return(poolable);
        }

        public void ReturnAll()
        {
            _pools.ForEach(pool => pool.Value.ReturnAll());
            _busyObjects.Clear();
        }
    }
}
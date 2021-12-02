using System;
using System.Collections.Generic;
using System.Linq;

namespace HighwayRage
{
    public class PoolCore<T> : IPool<T> where T : IPoolable<T>
    {
        private readonly HashSet<T> _allCreatedObjects = new HashSet<T>();
        private readonly Stack<T> _availableObjects = new Stack<T>();

        private readonly IPoolFactory<T> _objectsFactory;
        
        public PoolCore(IPoolFactory<T> factory)
        {
            _objectsFactory = factory;
        }

        public PoolCore(IPoolFactory<T> factory, int prewarm) : this(factory)
        {
            for (int i = 0; i < prewarm; i++)
            {
                T poolable = _objectsFactory.Create();
                poolable.Despawn();
                _allCreatedObjects.Add(poolable);
                _availableObjects.Push(poolable);
            }
        }

        public T Get()
        {
            T poolable;

            if (_availableObjects.Count > 0)
            {
                poolable = _availableObjects.Pop();
            }
            else
            {
                poolable = _objectsFactory.Create();
                _allCreatedObjects.Add(poolable);
            }
            
            return poolable;
        }

        public void Return(T poolable)
        {
            if (_allCreatedObjects.Contains(poolable) == false)
                throw new ArgumentException(nameof(poolable));
            
            _availableObjects.Push(poolable);
            poolable.Despawn();
        }

        public void ReturnAll()
        {
            _allCreatedObjects
                .Where(obj => _availableObjects.Contains(obj) == false)
                .ForEach(Return);
        }
    }
}
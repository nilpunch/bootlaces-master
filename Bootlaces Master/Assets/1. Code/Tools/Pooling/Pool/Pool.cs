using System;
using System.Collections.Generic;
using System.Linq;

namespace HighwayRage
{
    public class Pool<T> : IPool<T>
    {
        private readonly Stack<T> _availableObjects = new Stack<T>();
        private readonly IPoolFactory<T> _poolFactory;
        
        public Pool(IPoolFactory<T> poolFactory)
        {
            _poolFactory = poolFactory;
        }

        public T Get()
        {
            if (_availableObjects.Count > 0)
            {
                return _availableObjects.Pop();
            }
            
            return _poolFactory.Create();
        }

        public void Return(T item)
        {
            _availableObjects.Push(item);
        }
    }
}
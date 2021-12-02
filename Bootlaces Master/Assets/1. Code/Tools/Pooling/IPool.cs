namespace HighwayRage
{
    public interface IPool<in T> where T : IPoolable<T>
    {
        void Return(T poolable);
    }
}
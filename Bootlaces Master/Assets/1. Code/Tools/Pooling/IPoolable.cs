namespace HighwayRage
{
    public interface IPoolable<out T> where T : IPoolable<T>
    {
        void SpawnFrom(IPool<T> pool);
        void Despawn();
    }
}
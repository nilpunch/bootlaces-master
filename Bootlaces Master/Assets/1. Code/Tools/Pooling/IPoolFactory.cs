namespace HighwayRage
{
    public interface IPoolFactory<out T> where T : IPoolable<T>
    {
        T Create();
    }
}
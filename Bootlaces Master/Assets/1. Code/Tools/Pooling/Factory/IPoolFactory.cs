namespace HighwayRage
{
    public interface IPoolFactory<out T>
    {
        T Create();
    }
}
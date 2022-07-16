namespace HighwayRage
{
    public interface IPool<TItem>
    {
        TItem Get();
        void Return(TItem item);
    }
}
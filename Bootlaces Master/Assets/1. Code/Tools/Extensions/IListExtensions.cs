using System.Collections;

public static class IListExtensions
{
    public static void Shuffle(this IList list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            list.Swap(i - 1, UnityEngine.Random.Range(i, list.Count));
        }
    }
    
    public static void Swap(this IList list, int first, int second)
    {
        var temp = list[first];
        list[first] = list[second];
        list[second] = temp;
    }
}
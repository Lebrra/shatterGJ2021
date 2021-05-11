using System;
using System.Collections.Generic;

public static class IListExtensions
{
    public static int MinIndex<T>(this IList<T> list, Func<T, float> criterion)
    {
        int minimumIndex = 0;
        float minimum = criterion(list[0]);

        for(int i = 1; i < list.Count; i++)
        {
            float current = criterion(list[i]);
            if(current < minimum)
            {
                minimumIndex = i;
                minimum = current;
            }
        }

        return minimumIndex;
    }
}

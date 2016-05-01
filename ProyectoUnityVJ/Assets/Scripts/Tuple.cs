using UnityEngine;
using System.Collections;

public class Tuple<T1,T2>
{
    private T1 first;
    private T2 second;

    public Tuple(T1 item1, T2 item2)
    {
        first = item1;
        second = item2;
    }

    public T1 First()
    {
        return first;
    }

    public T2 Second()
    {
        return second;
    }
}

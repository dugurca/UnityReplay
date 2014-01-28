using UnityEngine;
using System.Collections;

public struct Doubles<X, Y>
{
    public X first;
    public Y second;

    public Doubles(X x, Y y)
    {
        first = x;
        second = y;
    }
}

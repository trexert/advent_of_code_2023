using System.Diagnostics.Contracts;
using System.Numerics;

static class Lib
{
    public static T Lcm<T>(List<T> values) where T : IBinaryInteger<T>
    {
        return values.Aggregate((acc, value) => Lcm(acc, value));
    }

    public static T Lcm<T>(T a, T b) where T : IBinaryInteger<T>
    {
        return a * b / Gcd(a, b);
    }

    public static T Gcd<T>(List<T> values) where T : IBinaryInteger<T>
    {
        return values.Aggregate((T acc, T value) => Gcd(acc, value));
    }

    public static T Gcd<T>(T a, T b) where T : IBinaryInteger<T>
    {
        while (a.CompareTo(default) > 0 && b.CompareTo(default) > 0)
        {
            if (a > b)
            {
                a %= b;
            }
            else
            {
                b %= a;
            }
        }
        return a | b;
    }

    public static IEnumerable<List<T>> Windows<T>(this IEnumerable<T> values, int windowSize)
    {
        var iter = values.GetEnumerator();

        List<T> window = [];
        while (iter.MoveNext())
        {
            window.Add(iter.Current);
            if (window.Count >= windowSize)
            {
                if (window.Count > windowSize)
                {
                    window = window[1..];
                }
                yield return window;
            }
        }
        yield break;
    }
}
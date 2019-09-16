using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        foreach (var i in Counter(1, 10)) Console.WriteLine(i);
    }

    public static IEnumerable<int> Counter(int start, int end)
    {
        if (start >= end) throw new ArgumentOutOfRangeException("start must be less than end");

        return localCounter();

        IEnumerable<int> localCounter()
        {
            for (int i = start; i < end; i++)
                yield return i;
        }
    }
}
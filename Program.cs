using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        List<int> numbers = null;
        int? i = null;

        numbers ??= new List<int>();
        numbers.Add(i ??= 17);
        numbers.Add(i ??= 20);

        Console.WriteLine(string.Join(' ', numbers));
        Console.WriteLine(i);
    }
}
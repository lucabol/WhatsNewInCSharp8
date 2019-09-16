using System;
using System.Linq;

class Program
{
    public static void Main()
    {
        IndicesAndRanges1();
    }

    private static string[] words = new string[]
    {
                        // index from start    index from end
            "The",      // 0                   ^9
            "quick",    // 1                   ^8
            "brown",    // 2                   ^7
            "fox",      // 3                   ^6
            "jumped",   // 4                   ^5
            "over",     // 5                   ^4
            "the",      // 6                   ^3
            "lazy",     // 7                   ^2
            "dog"       // 8                   ^1
    };

    public static void IndicesAndRanges1()
    {
        Console.WriteLine(words[^1]);
    }

    public static void IndicesAndRanges2()
    {
        var lazyDog = words[^2..^0];
        foreach (var word in lazyDog) Console.Write($"< {word} >");
    }

    public static void IndicesAndRanges3()
    {
        var allWords = words[..]; // contains "The" through "dog".
        var firstPhrase = words[..4]; // contains "The" through "fox"
        var lastPhrase = words[6..]; // contains "the, "lazy" and "dog"

        foreach (var word in allWords) Console.Write($"< {word} >");
        Console.WriteLine();
        foreach (var word in firstPhrase) Console.Write($"< {word} >");
        Console.WriteLine();
        foreach (var word in lastPhrase) Console.Write($"< {word} >");
    }

    public static void IndicesAndRanges4()
    {
        Index the = ^3;
        Console.WriteLine(words[the]);

        Range phrase = 1..4;
        var text = words[phrase];
        foreach (var word in text) Console.Write($"< {word} >");
    }

    public static void IndicesAndRanges5() // Why exclusive and ^0 end of collection
    {
        var numbers = Enumerable.Range(0, 100).ToArray();
        int x = 12;
        int y = 25;
        int z = 36;

        Console.WriteLine("===================== ^0 is the same as Length.     =>");
        Console.WriteLine($"{numbers[^x]} is the same as {numbers[numbers.Length - x]}");
        Console.WriteLine($"{numbers[x..y].Length} is the same as {y - x}");
        Console.WriteLine();

        Console.WriteLine("===================== Consecutive disjoint sequences.     =>");
        Console.WriteLine("numbers[x..y] and numbers[y..z] are consecutive and disjoint:");
        Span<int> x_y = numbers[x..y];
        Span<int> y_z = numbers[y..z];
        Console.WriteLine($"\tnumbers[x..y] is {x_y[0]} through {x_y[^1]}, numbers[y..z] is {y_z[0]} through {y_z[^1]}");
        Console.WriteLine();

        Console.WriteLine("===================== Remove elements from both ends.     =>");
        Console.WriteLine("numbers[x..^x] removes x elements at each end:");
        Span<int> x_x = numbers[x..^x];
        Console.WriteLine($"\tnumbers[x..^x] starts with {x_x[0]} and ends with {x_x[^1]}");
        Console.WriteLine();

        Console.WriteLine("===================== Incomplete sequences imply 0, ^0    =>");
        Console.WriteLine("numbers[..x] means numbers[0..x] and numbers[x..] means numbers[x..^0]");
        Span<int> start_x = numbers[..x];
        Span<int> zero_x = numbers[0..x];
        Console.WriteLine($"\t{start_x[0]}..{start_x[^1]} is the same as {zero_x[0]}..{zero_x[^1]}");
        Span<int> z_end = numbers[z..];
        Span<int> z_zero = numbers[z..];
        Console.WriteLine($"\t{z_end[0]}..{z_end[^1]} is the same as {z_zero[0]}..{z_zero[^1]}");
        Console.WriteLine();
    }

    public static void IndicesAndRanges6()
    {
        int[] sequence = Enumerable.Range(0, 1000).Select(x => (int)(Math.Sqrt(x) * 100)).ToArray();

        for (int start = 0; start < sequence.Length; start += 100)
        {
            Range r = start..(start + 10);
            var (min, max, average) = MovingAverage(sequence, r);
            Console.WriteLine($"From {r.Start} to {r.End}:    \tMin: {min},\tMax: {max},\tAverage: {average}");
        }

        for (int start = 0; start < sequence.Length; start += 100)
        {
            Range r = ^(start + 10)..^start;
            var (min, max, average) = MovingAverage(sequence, r);
            Console.WriteLine($"From {r.Start} to {r.End}:  \tMin: {min},\tMax: {max},\tAverage: {average}");
        }

        (int min, int max, double average) MovingAverage(int[] subSequence, Range range) =>
            (
                subSequence[range].Min(),
                subSequence[range].Max(),
                subSequence[range].Average()
            );
    }

}

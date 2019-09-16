using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

class Program
{
    public static async Task Main()
    {
        await AsyncStream();
    }

    internal static async IAsyncEnumerable<int> GenerateSequence()
    {
        for (int i = 0; i < 20; i++)
        {
            // every 3 elements, wait 2 seconds:
            if (i % 3 == 0)
                await Task.Delay(2000);
            yield return i;
        }
    }

    public async static Task AsyncStream()
    {
        await foreach (var number in GenerateSequence())
        {
            Console.WriteLine($"The time is {DateTime.Now:hh:mm:ss}. Retrieved {number}");
        }
    }
}
